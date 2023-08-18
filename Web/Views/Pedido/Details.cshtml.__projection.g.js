/* BEGIN EXTERNAL SOURCE */


        $("#btnCalificarVendedor").on('click', function () {

            var idPedido = $(this).data('idpedido');
            var idEvaluador = $(this).data('idevaluador');
            var evaluados = /*******************************/uar));
            var esOcultar = String($(this).html()).includes("Cancelar");

            if (!esOcultar) {
                var htmlToUse = "";

                //Se crea el html
                evaluados.forEach(evaluado => {

                    htmlToUse += `
                    <form id="evaluacionForm${evaluado.IdUsuario}" class="mb-3">
                                    <div class="p-3 rounded-4 bg-body card mb-4 shadow-lg bg-gradient">
                                        <label class="mb-2 form-label text-white">Evaluar a ${evaluado.Nombre}</label>
                                        <h3 class="card-title text-white-50 mb-2 fs-4">
                                            <input type="radio" id="estrella1${evaluado.IdUsuario}" name="Escala" value="1" style="display: none;">
                                            <label for="estrella1${evaluado.IdUsuario}"><i id="labelEstrella1${evaluado.IdUsuario}" class="fa-regular fa-star estrella${evaluado.IdUsuario}" num="1" style="cursor:pointer; color: white;"></i></label>
                                            <input type="radio" id="estrella2${evaluado.IdUsuario}" name="Escala" value="2" style="display: none;">
                                            <label for="estrella2${evaluado.IdUsuario}"><i id="labelEstrella2${evaluado.IdUsuario}" class="fa-regular fa-star estrella${evaluado.IdUsuario}" num="2" style="cursor:pointer; color: white;"></i></label>
                                            <input type="radio" id="estrella3${evaluado.IdUsuario}" name="Escala" value="3" style="display: none;">
                                            <label for="estrella3${evaluado.IdUsuario}"><i id="labelEstrella3${evaluado.IdUsuario}" class="fa-regular fa-star estrella${evaluado.IdUsuario}" num="3" style="cursor:pointer; color: white;"></i></label>
                                            <input type="radio" id="estrella4${evaluado.IdUsuario}" name="Escala" value="4" style="display: none;">
                                            <label for="estrella4${evaluado.IdUsuario}"><i id="labelEstrella4${evaluado.IdUsuario}" class="fa-regular fa-star estrella${evaluado.IdUsuario}" num="4" style="cursor:pointer; color: white;"></i></label>
                                            <input type="radio" id="estrella5${evaluado.IdUsuario}" name="Escala" value="5" style="display: none;">
                                            <label for="estrella5${evaluado.IdUsuario}"><i id="labelEstrella5${evaluado.IdUsuario}" class="fa-regular fa-star estrella${evaluado.IdUsuario}" num="5" style="cursor:pointer; color: white;"></i></label>
                                        </h3>
                                        <label id="Escala${evaluado.IdUsuario}-error" class="error"></label>
                                        <div class="mb-2">
                                            <label for="CodigoPostal" class="mb-2 form-label"> Escribir comentario</label>
                                            <textarea type="text" id="Comentario${evaluado.IdUsuario}" name="Comentario" class="form-control" placeholder="Fue un gran cliente"></textarea>
                                        </div>
                                        <button id="btnEnviarCalificacionCliente${evaluado.IdUsuario}" class="btn btn-primary my-2 my-sm-0 shadow-lg" type="submit" form="evaluacionForm${evaluado.IdUsuario}">Calificar</button>
                                    </div>
                    </form>
                `

                });

                $("#contenedorEvaluacionForm").html("");
                $("#contenedorEvaluacionForm").html(htmlToUse);

                //Se crean efectos de estrellas por form
                evaluados.forEach(evaluado => {

                    $(`.estrella${evaluado.IdUsuario}`).hover(
                        function () {
                            var numeroEstrella = Number($(this).attr('num'));

                            for (var i = 1; i <= numeroEstrella; i++) {
                                $(`#labelEstrella${i}${evaluado.IdUsuario}`).addClass('fa-solid');
                            }
                        },
                        function () {
                            var numeroEstrella = Number($(this).attr('num'));

                            for (var i = 1; i <= numeroEstrella; i++) {
                                $(`#labelEstrella${i}${evaluado.IdUsuario}`).removeClass('fa-solid');
                            }
                        }
                    );

                    $(`.estrella${evaluado.IdUsuario}`).on('click',
                        function () {

                            var numeroEstrella = Number($(this).attr('num'));

                            $(`.estrella${evaluado.IdUsuario}`).off("mouseenter mouseleave");

                            for (var i = 1; i <= 5; i++) {

                                if (i <= numeroEstrella) {
                                    $(`#labelEstrella${i}${evaluado.IdUsuario}`).addClass('fa-solid');
                                    $(`#labelEstrella${i}${evaluado.IdUsuario}`).css('color', 'rgb(255,215,0)');
                                }
                                else {
                                    $(`#labelEstrella${i}${evaluado.IdUsuario}`).removeClass('fa-solid');
                                    $(`#labelEstrella${i}${evaluado.IdUsuario}`).css('color', 'white');
                                }
                            }
                        }
                    );
                });

                //Se cran los formularios
                evaluados.forEach(evaluado => {

                    var idFormulario = `#evaluacionForm${evaluado.IdUsuario}`;

                    $(idFormulario).validate({
                        errorClass: "error",
                        rules: {
                            Comentario: {
                                required: true,
                                minlength: 10
                            }
                        },
                        messages: {
                            Comentario: "Debe escribir un comentario de mínimo 10 carárteres"
                        },
                        submitHandler: function (form, event) {
                            event.preventDefault();

                            var comentario = $(form).find('[name="Comentario"]').val();
                            var escala = $(form).find('[name="Escala"]:checked').val();
                            var idEvaluado = evaluado.IdUsuario;



                            if (escala === undefined) {
                                var avis/**************************************************************************************************************************/ing))"
                                eval(aviso);
                            }
                            else {

                                console.log(idEvaluador, idEvaluado, idPedido, comentario, escala)


                                $.ajax({
                                    method: "POST",
                                    url: "/Pedido/SaveEvaluacion",
                                    data: { idEvaluador, idEvaluado, idPedido, comentario, escala },
                                    success: function (result) {

                                        if (result.success) {



                                            $("#loaderSpinner").removeClass("d-none");

                                            $.ajax({
                                                method: "GET",
                                                url: "/Pedido/EvaluacionPedidoCliente",
                                                data: { idPedido, idUsuario: idEvaluador },
                                                success: function (html) {
                                                    $("#InfoEvaluaciones").html("");
                                                    $(`#InfoEvaluacionesTemp`).html("");
                                                    $(`#InfoEvaluacionesTemp`).html('<h2 class="text-center border-3 text-white rounded-4 w-auto mb-4 mt-4">Evaluaciones de Vendedores en el Pedido</h2><hr class= "mb-4"/>' + html);
                                                    $(idFormulario).hide();
                                                    $("#btnCalificarVendedor").hide();
                                                    $("#loaderSpinner").addClass("d-none");
                                                },
                                                error: function (xhr, status, error) {
                                                    console.log("error" + error, "no Error: " + xhr.status)

                                                }
                                            });

                                            console.log("Funcionó!!")
                                        }

                                    },
                                    error: function (xhr, status, error) {
                                        console.log("error" + error, "no Error: " + xhr.status)

                                    }
                                });

                            }
                        }
                    });
                });

                $(this).html("");
                $(this).html('<i class="fa-solid fa-x"></i> Cancelar');
            }
            else {
                $("#contenedorEvaluacionForm").html("");
                $(this).html("");
                $(this).html('<i class="fa-solid fa-star"></i> Calificar Vendedor(es)');
            }
        });

/* END EXTERNAL SOURCE */
/* BEGIN EXTERNAL SOURCE */


        $("#btnCalificarVendedor").on('click', function () {

            var idPedido = $(this).data('idpedido');
            var idEvaluador = $(this).data('idevaluador');
            var evaluados = /*******************************/uar));
            var esOcultar = String($(this).html()).includes("Cancelar");

            if (!esOcultar) {
                var htmlToUse = "";

                //Se crea el html
                evaluados.forEach(evaluado => {

                    htmlToUse += `
                    <form id="evaluacionForm${evaluado.IdUsuario}" class="mb-3">
                                    <div class="p-3 rounded-4 bg-body card mb-4 shadow-lg bg-gradient">
                                        <label class="mb-2 form-label text-white">Evaluar a ${evaluado.Nombre}</label>
                                        <h3 class="card-title text-white-50 mb-2 fs-4">
                                            <input type="radio" id="estrella1${evaluado.IdUsuario}" name="Escala" value="1" style="display: none;">
                                            <label for="estrella1${evaluado.IdUsuario}"><i id="labelEstrella1${evaluado.IdUsuario}" class="fa-regular fa-star estrella${evaluado.IdUsuario}" num="1" style="cursor:pointer; color: white;"></i></label>
                                            <input type="radio" id="estrella2${evaluado.IdUsuario}" name="Escala" value="2" style="display: none;">
                                            <label for="estrella2${evaluado.IdUsuario}"><i id="labelEstrella2${evaluado.IdUsuario}" class="fa-regular fa-star estrella${evaluado.IdUsuario}" num="2" style="cursor:pointer; color: white;"></i></label>
                                            <input type="radio" id="estrella3${evaluado.IdUsuario}" name="Escala" value="3" style="display: none;">
                                            <label for="estrella3${evaluado.IdUsuario}"><i id="labelEstrella3${evaluado.IdUsuario}" class="fa-regular fa-star estrella${evaluado.IdUsuario}" num="3" style="cursor:pointer; color: white;"></i></label>
                                            <input type="radio" id="estrella4${evaluado.IdUsuario}" name="Escala" value="4" style="display: none;">
                                            <label for="estrella4${evaluado.IdUsuario}"><i id="labelEstrella4${evaluado.IdUsuario}" class="fa-regular fa-star estrella${evaluado.IdUsuario}" num="4" style="cursor:pointer; color: white;"></i></label>
                                            <input type="radio" id="estrella5${evaluado.IdUsuario}" name="Escala" value="5" style="display: none;">
                                            <label for="estrella5${evaluado.IdUsuario}"><i id="labelEstrella5${evaluado.IdUsuario}" class="fa-regular fa-star estrella${evaluado.IdUsuario}" num="5" style="cursor:pointer; color: white;"></i></label>
                                        </h3>
                                        <label id="Escala${evaluado.IdUsuario}-error" class="error"></label>
                                        <div class="mb-2">
                                            <label for="CodigoPostal" class="mb-2 form-label"> Escribir comentario</label>
                                            <textarea type="text" id="Comentario${evaluado.IdUsuario}" name="Comentario" class="form-control" placeholder="Fue un gran cliente"></textarea>
                                        </div>
                                        <button id="btnEnviarCalificacionCliente${evaluado.IdUsuario}" class="btn btn-primary my-2 my-sm-0 shadow-lg" type="submit" form="evaluacionForm${evaluado.IdUsuario}">Calificar</button>
                                    </div>
                    </form>
                `

                });

                $("#contenedorEvaluacionForm").html("");
                $("#contenedorEvaluacionForm").html(htmlToUse);

                //Se crean efectos de estrellas por form
                evaluados.forEach(evaluado => {

                    $(`.estrella${evaluado.IdUsuario}`).hover(
                        function () {
                            var numeroEstrella = Number($(this).attr('num'));

                            for (var i = 1; i <= numeroEstrella; i++) {
                                $(`#labelEstrella${i}${evaluado.IdUsuario}`).addClass('fa-solid');
                            }
                        },
                        function () {
                            var numeroEstrella = Number($(this).attr('num'));

                            for (var i = 1; i <= numeroEstrella; i++) {
                                $(`#labelEstrella${i}${evaluado.IdUsuario}`).removeClass('fa-solid');
                            }
                        }
                    );

                    $(`.estrella${evaluado.IdUsuario}`).on('click',
                        function () {

                            var numeroEstrella = Number($(this).attr('num'));

                            $(`.estrella${evaluado.IdUsuario}`).off("mouseenter mouseleave");

                            for (var i = 1; i <= 5; i++) {

                                if (i <= numeroEstrella) {
                                    $(`#labelEstrella${i}${evaluado.IdUsuario}`).addClass('fa-solid');
                                    $(`#labelEstrella${i}${evaluado.IdUsuario}`).css('color', 'rgb(255,215,0)');
                                }
                                else {
                                    $(`#labelEstrella${i}${evaluado.IdUsuario}`).removeClass('fa-solid');
                                    $(`#labelEstrella${i}${evaluado.IdUsuario}`).css('color', 'white');
                                }
                            }
                        }
                    );
                });

                //Se cran los formularios
                evaluados.forEach(evaluado => {

                    var idFormulario = `#evaluacionForm${evaluado.IdUsuario}`;

                    $(idFormulario).validate({
                        errorClass: "error",
                        rules: {
                            Comentario: {
                                required: true,
                                minlength: 10
                            }
                        },
                        messages: {
                            Comentario: "Debe escribir un comentario de mínimo 10 carárteres"
                        },
                        submitHandler: function (form, event) {
                            event.preventDefault();

                            var comentario = $(form).find('[name="Comentario"]').val();
                            var escala = $(form).find('[name="Escala"]:checked').val();
                            var idEvaluado = evaluado.IdUsuario;



                            if (escala === undefined) {
                                var avis/**************************************************************************************************************************/ing))"
                                eval(aviso);
                            }
                            else {

                                console.log(idEvaluador, idEvaluado, idPedido, comentario, escala)


                                $.ajax({
                                    method: "POST",
                                    url: "/Pedido/SaveEvaluacion",
                                    data: { idEvaluador, idEvaluado, idPedido, comentario, escala },
                                    success: function (result) {

                                        if (result.success) {



                                            $("#loaderSpinner").removeClass("d-none");

                                            $.ajax({
                                                method: "GET",
                                                url: "/Pedido/EvaluacionPedidoCliente",
                                                data: { idPedido, idUsuario: idEvaluador },
                                                success: function (html) {
                                                    $("#InfoEvaluaciones").html("");
                                                    $(`#InfoEvaluacionesTemp`).html("");
                                                    $(`#InfoEvaluacionesTemp`).html('<h2 class="text-center border-3 text-white rounded-4 w-auto mb-4 mt-4">Evaluaciones de Vendedores en el Pedido</h2><hr class= "mb-4"/>' + html);
                                                    $(idFormulario).hide();
                                                    $("#btnCalificarVendedor").hide();
                                                    $("#loaderSpinner").addClass("d-none");
                                                },
                                                error: function (xhr, status, error) {
                                                    console.log("error" + error, "no Error: " + xhr.status)

                                                }
                                            });

                                            console.log("Funcionó!!")
                                        }

                                    },
                                    error: function (xhr, status, error) {
                                        console.log("error" + error, "no Error: " + xhr.status)

                                    }
                                });

                            }
                        }
                    });
                });

                $(this).html("");
                $(this).html('<i class="fa-solid fa-x"></i> Cancelar');
            }
            else {
                $("#contenedorEvaluacionForm").html("");
                $(this).html("");
                $(this).html('<i class="fa-solid fa-star"></i> Calificar Vendedor(es)');
            }
        });

/* END EXTERNAL SOURCE */
/* BEGIN EXTERNAL SOURCE */


        $("#btnCalificarVendedor").on('click', function () {

            var idPedido = $(this).data('idpedido');
            var idEvaluador = $(this).data('idevaluador');
            var evaluados = /*******************************/;
            var esOcultar = String($(this).html()).includes("Cancelar");

            if (!esOcultar) {
                var htmlToUse = "";

                //Se crea el html
                evaluados.forEach(evaluado => {

                    htmlToUse += `
                    <form id="evaluacionForm${evaluado.IdUsuario}" class="mb-3">
                                    <div class="p-3 rounded-4 bg-body card mb-4 shadow-lg bg-gradient">
                                        <label class="mb-2 form-label text-white">Evaluar a ${evaluado.Nombre}</label>
                                        <h3 class="card-title text-white-50 mb-2 fs-4">
                                            <input type="radio" id="estrella1${evaluado.IdUsuario}" name="Escala" value="1" style="display: none;">
                                            <label for="estrella1${evaluado.IdUsuario}"><i id="labelEstrella1${evaluado.IdUsuario}" class="fa-regular fa-star estrella${evaluado.IdUsuario}" num="1" style="cursor:pointer; color: white;"></i></label>
                                            <input type="radio" id="estrella2${evaluado.IdUsuario}" name="Escala" value="2" style="display: none;">
                                            <label for="estrella2${evaluado.IdUsuario}"><i id="labelEstrella2${evaluado.IdUsuario}" class="fa-regular fa-star estrella${evaluado.IdUsuario}" num="2" style="cursor:pointer; color: white;"></i></label>
                                            <input type="radio" id="estrella3${evaluado.IdUsuario}" name="Escala" value="3" style="display: none;">
                                            <label for="estrella3${evaluado.IdUsuario}"><i id="labelEstrella3${evaluado.IdUsuario}" class="fa-regular fa-star estrella${evaluado.IdUsuario}" num="3" style="cursor:pointer; color: white;"></i></label>
                                            <input type="radio" id="estrella4${evaluado.IdUsuario}" name="Escala" value="4" style="display: none;">
                                            <label for="estrella4${evaluado.IdUsuario}"><i id="labelEstrella4${evaluado.IdUsuario}" class="fa-regular fa-star estrella${evaluado.IdUsuario}" num="4" style="cursor:pointer; color: white;"></i></label>
                                            <input type="radio" id="estrella5${evaluado.IdUsuario}" name="Escala" value="5" style="display: none;">
                                            <label for="estrella5${evaluado.IdUsuario}"><i id="labelEstrella5${evaluado.IdUsuario}" class="fa-regular fa-star estrella${evaluado.IdUsuario}" num="5" style="cursor:pointer; color: white;"></i></label>
                                        </h3>
                                        <label id="Escala${evaluado.IdUsuario}-error" class="error"></label>
                                        <div class="mb-2">
                                            <label for="CodigoPostal" class="mb-2 form-label"> Escribir comentario</label>
                                            <textarea type="text" id="Comentario${evaluado.IdUsuario}" name="Comentario" class="form-control" placeholder="Fue un gran cliente"></textarea>
                                        </div>
                                        <button id="btnEnviarCalificacionCliente${evaluado.IdUsuario}" class="btn btn-primary my-2 my-sm-0 shadow-lg" type="submit" form="evaluacionForm${evaluado.IdUsuario}">Calificar</button>
                                    </div>
                    </form>
                `

                });

                $("#contenedorEvaluacionForm").html("");
                $("#contenedorEvaluacionForm").html(htmlToUse);

                //Se crean efectos de estrellas por form
                evaluados.forEach(evaluado => {

                    $(`.estrella${evaluado.IdUsuario}`).hover(
                        function () {
                            var numeroEstrella = Number($(this).attr('num'));

                            for (var i = 1; i <= numeroEstrella; i++) {
                                $(`#labelEstrella${i}${evaluado.IdUsuario}`).addClass('fa-solid');
                            }
                        },
                        function () {
                            var numeroEstrella = Number($(this).attr('num'));

                            for (var i = 1; i <= numeroEstrella; i++) {
                                $(`#labelEstrella${i}${evaluado.IdUsuario}`).removeClass('fa-solid');
                            }
                        }
                    );

                    $(`.estrella${evaluado.IdUsuario}`).on('click',
                        function () {

                            var numeroEstrella = Number($(this).attr('num'));

                            $(`.estrella${evaluado.IdUsuario}`).off("mouseenter mouseleave");

                            for (var i = 1; i <= 5; i++) {

                                if (i <= numeroEstrella) {
                                    $(`#labelEstrella${i}${evaluado.IdUsuario}`).addClass('fa-solid');
                                    $(`#labelEstrella${i}${evaluado.IdUsuario}`).css('color', 'rgb(255,215,0)');
                                }
                                else {
                                    $(`#labelEstrella${i}${evaluado.IdUsuario}`).removeClass('fa-solid');
                                    $(`#labelEstrella${i}${evaluado.IdUsuario}`).css('color', 'white');
                                }
                            }
                        }
                    );
                });

                //Se cran los formularios
                evaluados.forEach(evaluado => {

                    var idFormulario = `#evaluacionForm${evaluado.IdUsuario}`;

                    $(idFormulario).validate({
                        errorClass: "error",
                        rules: {
                            Comentario: {
                                required: true,
                                minlength: 10
                            }
                        },
                        messages: {
                            Comentario: "Debe escribir un comentario de mínimo 10 carárteres"
                        },
                        submitHandler: function (form, event) {
                            event.preventDefault();

                            var comentario = $(form).find('[name="Comentario"]').val();
                            var escala = $(form).find('[name="Escala"]:checked').val();
                            var idEvaluado = evaluado.IdUsuario;



                            if (escala === undefined) {
                                var aviso = "/**************************************************************************************************************************/"
                                eval(aviso);
                            }
                            else {

                                console.log(idEvaluador, idEvaluado, idPedido, comentario, escala)


                                $.ajax({
                                    method: "POST",
                                    url: "/Pedido/SaveEvaluacion",
                                    data: { idEvaluador, idEvaluado, idPedido, comentario, escala },
                                    success: function (result) {

                                        if (result.success) {



                                            $("#loaderSpinner").removeClass("d-none");

                                            $.ajax({
                                                method: "GET",
                                                url: "/Pedido/EvaluacionPedidoCliente",
                                                data: { idPedido, idUsuario: idEvaluador },
                                                success: function (html) {
                                                    $(`#InfoEvaluaciones`).html("");
                                                    $(`#InfoEvaluaciones`).html('<h2 class="text-center border-3 text-white rounded-4 w-auto mb-4 mt-4">Evaluaciones de Vendedores en el Pedido</h2><hr class= "mb-4"/>' + html);
                                                    $(idFormulario).hide();
                                                    $("#btnCalificarVendedor").hide();
                                                    $("#loaderSpinner").addClass("d-none");
                                                },
                                                error: function (xhr, status, error) {
                                                    console.log("error" + error, "no Error: " + xhr.status)

                                                }
                                            });

                                            console.log("Funcionó!!")
                                        }

                                    },
                                    error: function (xhr, status, error) {
                                        console.log("error" + error, "no Error: " + xhr.status)

                                    }
                                });

                            }
                        }
                    });
                });

                $(this).html("");
                $(this).html('<i class="fa-solid fa-x"></i> Cancelar');
            }
            else {
                $("#contenedorEvaluacionForm").html("");
                $(this).html("");
                $(this).html('<i class="fa-solid fa-star"></i> Calificar Vendedor(es)');
            }
        });

/* END EXTERNAL SOURCE */
