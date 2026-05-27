import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { MovimentoManualPage } from './features/movimentos-manuais/pages/movimento-manual-page/movimento-manual-page';

const routes: Routes = [
  {
    path: '',
    component: MovimentoManualPage
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
