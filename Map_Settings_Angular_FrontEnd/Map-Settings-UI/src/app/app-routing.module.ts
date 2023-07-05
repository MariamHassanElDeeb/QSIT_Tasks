import { NgModule, Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ErrorComponent } from './Components/error/error.component';
import { RegisterComponent } from './Components/register/register.component';
import { LoginComponent } from './Components/login/login.component';
import { CreateMapComponent } from './Components/create-map/create-map.component';
import { CreatedMapComponent } from './Components/Created-map/Created-map.component';
const routes: Routes = [
{path:"register",component:RegisterComponent},
{path:"login",component:LoginComponent},
{path:"create",component:CreateMapComponent},
{path:"created",component:CreatedMapComponent},
  {path:"**",component:ErrorComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
