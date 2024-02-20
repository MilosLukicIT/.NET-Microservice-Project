import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppRoutingModule } from './app.routing';
import { ComponentsModule } from './components/components.module';
import { AppComponent } from './app.component';
import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';
import { LoginComponent } from './login/login.component';
import { AuthconfigInterceptor } from './services/interceptor/authconfig.interceptor';
import { UserPageComponent } from './user-page/user-page.component';
import { MikimilaneComponent } from './services/mikimilane/mikimilane.component';

@NgModule({
  imports: [
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    ComponentsModule,
    RouterModule,
    AppRoutingModule
  ],
  declarations: [
    AppComponent,
    AdminLayoutComponent,
    MikimilaneComponent,
    
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS, 
    useClass: AuthconfigInterceptor, 
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
