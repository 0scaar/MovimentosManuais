import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { finalize } from 'rxjs';
import Swal from 'sweetalert2';

import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { Produto } from '../../models/produto.model';
import { ProdutoCosif } from '../../models/produto-cosif.model';
import { MovimentoManual } from '../../models/movimento-manual.model';
import { ProdutoService } from '../../services/produto.service';
import { ProdutoCosifService } from '../../services/produto-cosif.service';
import { MovimentoManualService } from '../../services/movimento-manual.service';

@Component({
  selector: 'app-movimento-manual-page',
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl: './movimento-manual-page.html',
  styleUrl: './movimento-manual-page.css'
})
export class MovimentoManualPage implements OnInit {
  form!: FormGroup;

  produtos: Produto[] = [];
  cosifs: ProdutoCosif[] = [];
  movimentos: MovimentoManual[] = [];

  carregando = false;
  carregandoProdutos = false;
  carregandoCosifs = false;
  editando = false;
  erroProdutos = '';
  erroCosifs = '';
  erroMovimentos = '';

  constructor(
    private readonly fb: FormBuilder,
    private readonly produtoService: ProdutoService,
    private readonly produtoCosifService: ProdutoCosifService,
    private readonly movimentoManualService: MovimentoManualService
  ) { }

  ngOnInit(): void {
    this.criarFormulario();
    this.carregarDadosIniciais();

    this.form.get('codigoProduto')?.valueChanges.subscribe(codigoProduto => {
      this.form.patchValue({ codigoCosif: '' });
      this.cosifs = [];
      this.erroCosifs = '';

      if (codigoProduto) {
        this.carregarCosifs(codigoProduto);
      }
    });
  }

  salvar(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.editando ? this.editar() : this.criar();
  }

  novo(): void {
    this.editando = false;
    this.form.get('mes')?.enable({ emitEvent: false });
    this.form.get('ano')?.enable({ emitEvent: false });
    this.form.get('numeroLancamento')?.enable({ emitEvent: false });
    this.form.get('codigoProduto')?.enable({ emitEvent: false });
    this.form.get('codigoCosif')?.enable({ emitEvent: false });

    this.form.reset({
      mes: '',
      ano: '',
      numeroLancamento: '',
      codigoProduto: '',
      codigoCosif: '',
      valor: '',
      descricao: '',
      codigoUsuario: 'Sistema'
    }, { emitEvent: false });

    this.cosifs = [];
    this.erroCosifs = '';
    this.carregandoCosifs = false;
  }

  selecionarParaEdicao(movimento: MovimentoManual): void {
    this.editando = true;
    this.carregandoCosifs = false;
    this.erroCosifs = '';
    this.cosifs = [{
      codigoProduto: movimento.codigoProduto,
      codigoCosif: movimento.codigoCosif,
      codigoClassificacao: movimento.descricaoCosif ?? movimento.codigoCosif
    }];

    this.form.patchValue({
      mes: movimento.mes,
      ano: movimento.ano,
      numeroLancamento: movimento.numeroLancamento,
      codigoProduto: movimento.codigoProduto,
      codigoCosif: movimento.codigoCosif,
      valor: movimento.valor,
      descricao: movimento.descricao,
      codigoUsuario: 'Sistema'
    }, { emitEvent: false });

    this.form.get('mes')?.disable({ emitEvent: false });
    this.form.get('ano')?.disable({ emitEvent: false });
    this.form.get('numeroLancamento')?.disable({ emitEvent: false });
    this.form.get('codigoProduto')?.disable({ emitEvent: false });
    this.form.get('codigoCosif')?.disable({ emitEvent: false });
  }

  excluir(movimento: MovimentoManual): void {
    Swal.fire({
      title: 'Confirmar exclusão?',
      text: `Deseja excluir o lançamento ${movimento.numeroLancamento}?`,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Sim, excluir',
      cancelButtonText: 'Cancelar'
    }).then(result => {
      if (!result.isConfirmed) {
        return;
      }

      this.movimentoManualService.excluir(movimento).subscribe({
        next: () => {
          Swal.fire('Sucesso', 'Movimento excluído com sucesso.', 'success');
          this.carregarMovimentos();
          this.novo();
        },
        error: error => this.exibirErro(error)
      });
    });
  }

  campoInvalido(campo: string): boolean {
    const control = this.form.get(campo);
    return !!control && control.invalid && (control.dirty || control.touched);
  }

  private criarFormulario(): void {
    this.form = this.fb.group({
      mes: ['', [Validators.required, Validators.min(1), Validators.max(12)]],
      ano: ['', [Validators.required, Validators.min(1900)]],
      numeroLancamento: [''],
      codigoProduto: ['', Validators.required],
      codigoCosif: ['', Validators.required],
      valor: ['', [Validators.required, Validators.min(0.01)]],
      descricao: ['', [Validators.required, Validators.maxLength(50)]],
      codigoUsuario: ['Sistema', [Validators.required, Validators.maxLength(15)]]
    });
  }

  private carregarProdutos(): void {
    this.carregandoProdutos = true;
    this.erroProdutos = '';

    this.produtoService.listarAtivos()
      .pipe(finalize(() => this.carregandoProdutos = false))
      .subscribe({
        next: produtos => this.produtos = produtos,
        error: error => {
          this.produtos = [];
          this.erroProdutos = this.obterMensagemErro(error);
        }
      });
  }

  private carregarCosifs(codigoProduto: string): void {
    this.carregandoCosifs = true;
    this.erroCosifs = '';

    this.produtoCosifService.listarPorProduto(codigoProduto)
      .pipe(finalize(() => this.carregandoCosifs = false))
      .subscribe({
        next: cosifs => this.cosifs = cosifs,
        error: error => {
          this.cosifs = [];
          this.erroCosifs = this.obterMensagemErro(error);
        }
      });
  }

  private carregarMovimentos(): void {
    this.carregando = true;
    this.erroMovimentos = '';

    this.movimentoManualService.listar()
      .pipe(finalize(() => this.carregando = false))
      .subscribe({
        next: movimentos => this.movimentos = movimentos,
        error: error => {
          this.movimentos = [];
          this.erroMovimentos = this.obterMensagemErro(error);
        }
      });
  }

  private carregarDadosIniciais(): void {
    this.carregarProdutos();
    this.carregarMovimentos();
  }

  private criar(): void {
    const request = this.form.getRawValue();

    this.movimentoManualService.criar(request).subscribe({
      next: () => {
        Swal.fire('Sucesso', 'Movimento criado com sucesso.', 'success');
        this.carregarMovimentos();
        this.novo();
      },
      error: error => this.exibirErro(error)
    });
  }

  private editar(): void {
    const formValue = this.form.getRawValue();
    const chave = {
      mes: formValue.mes,
      ano: formValue.ano,
      numeroLancamento: formValue.numeroLancamento,
      codigoProduto: formValue.codigoProduto,
      codigoCosif: formValue.codigoCosif
    };
    const request = {
      valor: formValue.valor,
      descricao: formValue.descricao,
      codigoUsuario: formValue.codigoUsuario
    };

    this.movimentoManualService.editar(chave, request).subscribe({
      next: () => {
        Swal.fire('Sucesso', 'Movimento editado com sucesso.', 'success');
        this.carregarMovimentos();
        this.novo();
      },
      error: error => this.exibirErro(error)
    });
  }

  private exibirErro(error: any): void {
    const message = this.obterMensagemErro(error);

    Swal.fire('Erro', message, 'error');
  }

  private obterMensagemErro(error: any): string {
    return error?.error?.message ||
      error?.error?.errors?.join('<br>') ||
      'Erro inesperado.';
  }
}
