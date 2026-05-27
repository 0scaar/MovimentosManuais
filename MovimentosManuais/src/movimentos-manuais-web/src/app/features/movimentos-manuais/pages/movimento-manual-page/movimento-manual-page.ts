import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
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
  editando = false;

  constructor(
    private readonly fb: FormBuilder,
    private readonly produtoService: ProdutoService,
    private readonly produtoCosifService: ProdutoCosifService,
    private readonly movimentoManualService: MovimentoManualService
  ) { }

  ngOnInit(): void {
    this.criarFormulario();
    this.carregarProdutos();
    this.carregarMovimentos();

    this.form.get('codigoProduto')?.valueChanges.subscribe(codigoProduto => {
      this.form.patchValue({ codigoCosif: '' });
      this.cosifs = [];

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
    this.form.reset({
      mes: '',
      ano: '',
      numeroLancamento: '',
      codigoProduto: '',
      codigoCosif: '',
      valor: '',
      descricao: '',
      codigoUsuario: 'sistema'
    });

    this.form.get('mes')?.enable();
    this.form.get('ano')?.enable();
    this.form.get('numeroLancamento')?.enable();
  }

  selecionarParaEdicao(movimento: MovimentoManual): void {
    this.editando = true;

    this.carregarCosifs(movimento.codigoProduto);

    this.form.patchValue({
      mes: movimento.mes,
      ano: movimento.ano,
      numeroLancamento: movimento.numeroLancamento,
      codigoProduto: movimento.codigoProduto,
      codigoCosif: movimento.codigoCosif,
      valor: movimento.valor,
      descricao: movimento.descricao,
      codigoUsuario: movimento.codigoUsuario
    });

    this.form.get('mes')?.disable();
    this.form.get('ano')?.disable();
    this.form.get('numeroLancamento')?.disable();
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
      codigoUsuario: ['sistema', [Validators.required, Validators.maxLength(15)]]
    });
  }

  private carregarProdutos(): void {
    this.produtoService.listarAtivos().subscribe({
      next: produtos => this.produtos = produtos,
      error: error => this.exibirErro(error)
    });
  }

  private carregarCosifs(codigoProduto: string): void {
    this.produtoCosifService.listarPorProduto(codigoProduto).subscribe({
      next: cosifs => this.cosifs = cosifs,
      error: error => this.exibirErro(error)
    });
  }

  private carregarMovimentos(): void {
    this.carregando = true;

    this.movimentoManualService.listar().subscribe({
      next: movimentos => {
        this.movimentos = movimentos;
        this.carregando = false;
      },
      error: error => {
        this.carregando = false;
        this.exibirErro(error);
      }
    });
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
    const request = this.form.getRawValue();

    this.movimentoManualService.editar(request).subscribe({
      next: () => {
        Swal.fire('Sucesso', 'Movimento editado com sucesso.', 'success');
        this.carregarMovimentos();
        this.novo();
      },
      error: error => this.exibirErro(error)
    });
  }

  private exibirErro(error: any): void {
    const message =
      error?.error?.message ||
      error?.error?.errors?.join('<br>') ||
      'Erro inesperado.';

    Swal.fire('Erro', message, 'error');
  }
}
