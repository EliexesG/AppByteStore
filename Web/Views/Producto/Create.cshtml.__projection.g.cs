//------------------------------------------------------------------------------
// <auto-generated>
//     Este c�digo fue generado por una herramienta.
//     Versi�n de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podr�an causar un comportamiento incorrecto y se perder�n si
//     se vuelve a generar el c�digo.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ASP {
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Security;
using System.Web.UI;
using System.Web.WebPages;
using System.Web.WebPages.Html;

public class _Page_Create_cshtml : System.Web.WebPages.WebPage {
private static object @__o;
#line hidden
public _Page_Create_cshtml() {
}
protected System.Web.HttpApplication ApplicationInstance {
get {
return ((System.Web.HttpApplication)(Context.ApplicationInstance));
}
}
public override void Execute() {

#line 1 "C:\Users\fabyv\AppData\Local\Temp\5FB4C7C3BA15DB73E9EE1CDD36AA399D20F0\2\AppByteStore\Web\Views\Producto\Create.cshtml"
__o = model;


#line default
#line hidden

#line 2 "C:\Users\fabyv\AppData\Local\Temp\5FB4C7C3BA15DB73E9EE1CDD36AA399D20F0\2\AppByteStore\Web\Views\Producto\Create.cshtml"
  
    ViewBag.Title = "ByteStore - Publica tu Producto";


#line default
#line hidden

#line 3 "C:\Users\fabyv\AppData\Local\Temp\5FB4C7C3BA15DB73E9EE1CDD36AA399D20F0\2\AppByteStore\Web\Views\Producto\Create.cshtml"
using (Html.BeginForm("Save", "Producto", FormMethod.Post, new { enctype = "multipart/form-data" }))
{
    

#line default
#line hidden

#line 4 "C:\Users\fabyv\AppData\Local\Temp\5FB4C7C3BA15DB73E9EE1CDD36AA399D20F0\2\AppByteStore\Web\Views\Producto\Create.cshtml"
__o = Html.AntiForgeryToken();


#line default
#line hidden

#line 5 "C:\Users\fabyv\AppData\Local\Temp\5FB4C7C3BA15DB73E9EE1CDD36AA399D20F0\2\AppByteStore\Web\Views\Producto\Create.cshtml"
                            

    

#line default
#line hidden

#line 6 "C:\Users\fabyv\AppData\Local\Temp\5FB4C7C3BA15DB73E9EE1CDD36AA399D20F0\2\AppByteStore\Web\Views\Producto\Create.cshtml"
           __o = Html.LabelFor(model => model.Nombre, htmlAttributes: new { @class = "control-label col-md-2 mb-2" });


#line default
#line hidden

#line 7 "C:\Users\fabyv\AppData\Local\Temp\5FB4C7C3BA15DB73E9EE1CDD36AA399D20F0\2\AppByteStore\Web\Views\Producto\Create.cshtml"
               __o = Html.EditorFor(model => model.Nombre, new { htmlAttributes = new { @class = "form-control", @placeholder = "Mario Bros 2" } });


#line default
#line hidden

#line 8 "C:\Users\fabyv\AppData\Local\Temp\5FB4C7C3BA15DB73E9EE1CDD36AA399D20F0\2\AppByteStore\Web\Views\Producto\Create.cshtml"
               __o = Html.ValidationMessageFor(model => model.Nombre, "", new { @class = "text-danger" });


#line default
#line hidden

#line 9 "C:\Users\fabyv\AppData\Local\Temp\5FB4C7C3BA15DB73E9EE1CDD36AA399D20F0\2\AppByteStore\Web\Views\Producto\Create.cshtml"
           __o = Html.LabelFor(model => model.Categoria, htmlAttributes: new { @class = "control-label col-md-2 mb-2" });


#line default
#line hidden

#line 10 "C:\Users\fabyv\AppData\Local\Temp\5FB4C7C3BA15DB73E9EE1CDD36AA399D20F0\2\AppByteStore\Web\Views\Producto\Create.cshtml"
               __o = Html.DropDownListFor(model => model.Categoria.IdCategoria, (SelectList)ViewBag.listaCategoria, "Seleccione una Categor�a", htmlAttributes: new { @class = "form-select form-control me-sm-2 text-black" });


#line default
#line hidden

#line 11 "C:\Users\fabyv\AppData\Local\Temp\5FB4C7C3BA15DB73E9EE1CDD36AA399D20F0\2\AppByteStore\Web\Views\Producto\Create.cshtml"
               __o = Html.ValidationMessageFor(model => model.Categoria.IdCategoria, "", new { @class = "text-danger" });


#line default
#line hidden

#line 12 "C:\Users\fabyv\AppData\Local\Temp\5FB4C7C3BA15DB73E9EE1CDD36AA399D20F0\2\AppByteStore\Web\Views\Producto\Create.cshtml"
           __o = Html.LabelFor(model => model.Descripcion, htmlAttributes: new { @class = "control-label col-md-2 mb-2" });


#line default
#line hidden

#line 13 "C:\Users\fabyv\AppData\Local\Temp\5FB4C7C3BA15DB73E9EE1CDD36AA399D20F0\2\AppByteStore\Web\Views\Producto\Create.cshtml"
               __o = Html.TextAreaFor(model => model.Descripcion, new { @class = "form-control", @style = "min-height: 100px;", placeholder = "Un juego de aventuras" });


#line default
#line hidden

#line 14 "C:\Users\fabyv\AppData\Local\Temp\5FB4C7C3BA15DB73E9EE1CDD36AA399D20F0\2\AppByteStore\Web\Views\Producto\Create.cshtml"
               __o = Html.ValidationMessageFor(model => model.Descripcion, "", new { @class = "text-danger" });


#line default
#line hidden

#line 15 "C:\Users\fabyv\AppData\Local\Temp\5FB4C7C3BA15DB73E9EE1CDD36AA399D20F0\2\AppByteStore\Web\Views\Producto\Create.cshtml"
           __o = Html.LabelFor(model => model.Precio, htmlAttributes: new { @class = "control-label col-md-2 mb-2" });


#line default
#line hidden

#line 16 "C:\Users\fabyv\AppData\Local\Temp\5FB4C7C3BA15DB73E9EE1CDD36AA399D20F0\2\AppByteStore\Web\Views\Producto\Create.cshtml"
                   __o = Html.EditorFor(model => model.Precio, new { htmlAttributes = new { @class = "form-control", @placeholder="2500" } });


#line default
#line hidden

#line 17 "C:\Users\fabyv\AppData\Local\Temp\5FB4C7C3BA15DB73E9EE1CDD36AA399D20F0\2\AppByteStore\Web\Views\Producto\Create.cshtml"
               __o = Html.ValidationMessageFor(model => model.Precio, "", new { @class = "text-danger" });


#line default
#line hidden

#line 18 "C:\Users\fabyv\AppData\Local\Temp\5FB4C7C3BA15DB73E9EE1CDD36AA399D20F0\2\AppByteStore\Web\Views\Producto\Create.cshtml"
           __o = Html.LabelFor(model => model.Stock, htmlAttributes: new { @class = "control-label col-md-2  mb-2" });


#line default
#line hidden

#line 19 "C:\Users\fabyv\AppData\Local\Temp\5FB4C7C3BA15DB73E9EE1CDD36AA399D20F0\2\AppByteStore\Web\Views\Producto\Create.cshtml"
               __o = Html.EditorFor(model => model.Stock, new { htmlAttributes = new { @class = "form-control", @placeholder = "5" } });


#line default
#line hidden

#line 20 "C:\Users\fabyv\AppData\Local\Temp\5FB4C7C3BA15DB73E9EE1CDD36AA399D20F0\2\AppByteStore\Web\Views\Producto\Create.cshtml"
               __o = Html.ValidationMessageFor(model => model.Stock, "", new { @class = "text-danger" });


#line default
#line hidden

#line 21 "C:\Users\fabyv\AppData\Local\Temp\5FB4C7C3BA15DB73E9EE1CDD36AA399D20F0\2\AppByteStore\Web\Views\Producto\Create.cshtml"
           __o = Html.LabelFor(model => model.Estado, htmlAttributes: new { @class = "control-label col-md-2 mb-2" });


#line default
#line hidden

#line 22 "C:\Users\fabyv\AppData\Local\Temp\5FB4C7C3BA15DB73E9EE1CDD36AA399D20F0\2\AppByteStore\Web\Views\Producto\Create.cshtml"
               __o = Html.DropDownListFor(model => model.Estado, (SelectList)ViewBag.listaEstados, "Seleccione un Estado", htmlAttributes: new { @class = "form-select form-control me-sm-2 text-black" });


#line default
#line hidden

#line 23 "C:\Users\fabyv\AppData\Local\Temp\5FB4C7C3BA15DB73E9EE1CDD36AA399D20F0\2\AppByteStore\Web\Views\Producto\Create.cshtml"
               __o = Html.ValidationMessageFor(model => model.Estado, "", new { @class = "text-danger" });


#line default
#line hidden

#line 24 "C:\Users\fabyv\AppData\Local\Temp\5FB4C7C3BA15DB73E9EE1CDD36AA399D20F0\2\AppByteStore\Web\Views\Producto\Create.cshtml"
           __o = Html.LabelFor(model => model.FotoProducto, htmlAttributes: new { @class = "control-label mb-2" });


#line default
#line hidden

#line 25 "C:\Users\fabyv\AppData\Local\Temp\5FB4C7C3BA15DB73E9EE1CDD36AA399D20F0\2\AppByteStore\Web\Views\Producto\Create.cshtml"
               __o = Html.ValidationMessageFor(model => model.FotoProducto, "", new { @class = "text-danger" });


#line default
#line hidden

#line 26 "C:\Users\fabyv\AppData\Local\Temp\5FB4C7C3BA15DB73E9EE1CDD36AA399D20F0\2\AppByteStore\Web\Views\Producto\Create.cshtml"
                        __o = Url.Action("IndexVendedor","Producto");


#line default
#line hidden

#line 27 "C:\Users\fabyv\AppData\Local\Temp\5FB4C7C3BA15DB73E9EE1CDD36AA399D20F0\2\AppByteStore\Web\Views\Producto\Create.cshtml"
          
}

#line default
#line hidden
DefineSection("Scripts", () => {


#line 28 "C:\Users\fabyv\AppData\Local\Temp\5FB4C7C3BA15DB73E9EE1CDD36AA399D20F0\2\AppByteStore\Web\Views\Producto\Create.cshtml"
__o = Scripts.Render("~/bundles/jqueryval");


#line default
#line hidden
});

}
}
}
