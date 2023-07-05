import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ErrorComponent } from './Components/error/error.component';
import { HeaderComponent } from './Components/header/header.component';
import {HttpClientModule} from '@angular/common/http'
import { RegisterComponent } from './Components/register/register.component';
import { LoginComponent } from './Components/login/login.component';
import { CreateMapComponent } from './Components/create-map/create-map.component';
import { AuthInterceptorProviders } from './Middlewares/auth-interceptor';
import { CreatedMapComponent } from './Components/Created-map/Created-map.component';

@NgModule({
  declarations: [
    AppComponent,
    ErrorComponent,
    HeaderComponent,
    RegisterComponent,
    LoginComponent,
    CreateMapComponent,
    CreatedMapComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [AuthInterceptorProviders
    /**
     * 5- Service
     */
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
