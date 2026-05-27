import { NgModule, provideBrowserGlobalErrorListeners } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import { MovimentoManualPage } from './features/movimentos-manuais/pages/movimento-manual-page/movimento-manual-page';

@NgModule({
  declarations: [App], //, MovimentoManualPage],
  imports: [BrowserModule, AppRoutingModule, HttpClientModule, FormsModule, ReactiveFormsModule, MovimentoManualPage],
  providers: [provideBrowserGlobalErrorListeners()],
  bootstrap: [App],
})
export class AppModule {}
