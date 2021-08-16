import { CUSTOM_ELEMENTS_SCHEMA, NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoadingButtonComponent } from './components/buttons/loading-button/loading-button.component';
import { PagingSectionComponent } from './components/paging-section/paging-section.component';
import { AlreadySignedGuard } from './helpers/Guards/already-signed.guard';
import { AuthGuard } from './helpers/Guards/auth.guard';


@NgModule({
  declarations: [LoadingButtonComponent,PagingSectionComponent],
  imports: [
    CommonModule
  ],
  providers:[
    AlreadySignedGuard,
    AuthGuard,
  ],
  exports:[
    LoadingButtonComponent,PagingSectionComponent
  ],
  schemas:[NO_ERRORS_SCHEMA,CUSTOM_ELEMENTS_SCHEMA],
})
export class SharedModule { }
