import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ToastrModule } from "ngx-toastr";

import { SidebarModule } from './sidebar/sidebar.module';
import { FooterModule } from './shared/footer/footer.module';
import { NavbarModule} from './shared/navbar/navbar.module';
import { FixedPluginModule} from './shared/fixedplugin/fixedplugin.module';

import { AppComponent } from './app.component';
import { AppRoutes } from './app.routing';

import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';
import { AuthService } from "./shared/Services/auth.service";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { LoaderService } from "./shared/Services/loader-service.service";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { ErrorInterceptor } from "./shared/helpers/error-interceptor";
import { CommonUtilityService } from "./shared/Services/commonUtility.service";
import { ToastService } from "./shared/Services/toast.service.";
import { CancelHttpInterceptor } from "./shared/helpers/cancelHttp.Interceptor";
import { JwtInterceptor } from "./shared/helpers/Jwt.Interceptor";
import { LoaderInterceptor } from "./shared/helpers/loader.interceptor";
import { CancelHttpService } from "./shared/Services/cancelHttp.service";


@NgModule({
  declarations: [
    AppComponent,
    AdminLayoutComponent
  ],
  imports: [
    BrowserAnimationsModule,
    RouterModule.forRoot(AppRoutes),
    SidebarModule,
    NavbarModule,
    ToastrModule.forRoot(),
    FooterModule,
    FixedPluginModule,
    HttpClientModule,
    ReactiveFormsModule,
    
  ],
  providers: [AuthService,LoaderService,
    CommonUtilityService,
    ToastService,
    CancelHttpService,
    AuthService,
    {provide:HTTP_INTERCEPTORS,useClass:ErrorInterceptor,multi:true},
     {provide:HTTP_INTERCEPTORS,useClass:CancelHttpInterceptor,multi:true},
     {provide:HTTP_INTERCEPTORS,useClass:LoaderInterceptor,multi:true},
    {provide:HTTP_INTERCEPTORS,useClass:JwtInterceptor,multi:true}],
  bootstrap: [AppComponent]
})
export class AppModule { }
