/* BEGIN EXTERNAL SOURCE */



        $(document).ready(function () {

            $.ajax({
                method: "GET",
                url: "/Reportes/ReporteCompras",
                data: {},
                success: function (result) {

                    var ctx = $('#comprasDia')[0].getContext('2d');
                    var myChart = new Chart(ctx, {
                        type: 'bar',
                        data: {
                            labels: ['Hoy'],
                            datasets: [{
                                label: 'Compras registradas en el día',
                                data: [result.lista.length],
                                backgroundColor: 'rgba(255,215,0, 0.5)',
                                borderColor: 'rgba(255,215,0,1)',
                                borderWidth: 2
                            }]
                        },
                        options: {
                            scales: {
                                y: {
                                    beginAtZero: true
                                }
                                          method: "GET",
                url: "/Reportes/GetTopProductosVendidosByMes",
                data: {},
                success: function (result) {

                    var ctx = $('#topProductosVendidosMes')[0].getContext('2d');
                    var myChart = new Chart(ctx, {
                        type: 'bar',
                        data: {
                            labels: result.lista.map(p => p.NombreProducto),
                            datasets: [{
                                label: 'Cantidad de veces vendido al mes',
                                data: result.lista.map(p => p.TotalVendidos),
                                backgroundColor: 'rgba(75, 192, 192, 0.5)',
                                borderColor: 'rgba(75, 192, 192, 1)',
          Productos más comprados delWidth: 2
                            }]
                        },
                        options: {
                            scales: 43, 94, 33                       y: {
                                43, 94, 33 ero: true
                                }
                            }
                        }
                    });

                },
                error: function (xhr, status, error) {
                    console.log("error" + error, "no Error: " + xhr.status)
                }
            });

            $.ajax({
                method: "GET",
                url: "/Reportes/GetVendedoresMejorEvaluados",
                data: {},
                success: function (result) {

                    console.log(result)

                    var ctx = $('#mejorEvaluacion')[0].getContext('2d');
                    var myChart = new Chart(ctx, {
                        type: 'bar',
                        data: {
                            labels: result.lista.map(p => `${p.Nombre} ${p.PrimerApellido}`),
                            datasets: [{
                                label: 'Promedio evaluado',
                                data: result.lista.map(p => p.PromedioEvaluaciones),
                                backgroundColor: 'rgba(75, 192, 192, 0.5)',
                                borderColor: 'rgba(75, 192, 192, 1)',
                  Vendedores con mejor evaluación derWidth: 2
                            }]
                        },
                        options: {
                            scales: 46, 175, 249                       y: {
                                46, 175, 249ero: true
                                }
                            }
                        }
                    });

                },
                error: function (xhr, status, error) {
                    console.log("error" + error, "no Error: " + xhr.status)
                }
            });

            $.ajax({
                method: "GET",
                url: "/Reportes/GetTopProductosVendidosByMes",
                data: {},
                success: function (result) {

                    var ctx = $('#topProductosVendidosMes')[0].getContext('2d');
                    var myChart = new Chart(ctx, {
                        type: 'bar',
                        data: {
                            labels: result.lista.map(p => p.NombreProducto),
                            datasets: [{
                                label: 'Cantidad de veces vendido al mes',
                                data: result.lista.map(p => p.TotalVendidos),
                                backgroundColor: 'rgba(75, 192, 192, 0.5)',
                                borderColor: 'rgba(75, 192, 192, 1)',
                Vendedores peores calificadosorderWidth: 2
                            }]
                        },
                        options: {
                            scales249, 46, 65                         y: {
                              255, 0, 23 tZero: true
                                }
                            }
                        }
                    });

                },
                error: function (xhr, status, error) {
                    console.log("error" + error, "no Error: " + xhr.status)
                }
            });
        });

    
/* END EXTERNAL SOURCE */
/* BEGIN EXTERNAL SOURCE */



        $(document).ready(function () {

            $.ajax({
                method: "GET",
                url: "/Reportes/ReporteCompras",
                data: {},
                success: function (result) {

                    var ctx = $('#comprasDia')[0].getContext('2d');
                    var myChart = new Chart(ctx, {
                        type: 'bar',
                        data: {
                            labels: ['Hoy'],
                            datasets: [{
                                label: 'Compras registradas en el día',
                                data: [result.lista.length],
                                backgroundColor: 'rgba(255,215,0, 0.5)',
                                borderColor: 'rgba(255,215,0,1)',
                                borderWidth: 2
                            }]
                        },
                        options: {
                            scales: {
                                y: {
                                    beginAtZero: true
                                }
                            }
                        }
                    });

                },
                error: function (xhr, status, error) {
                    console.log("error" + error, "no Error: " + xhr.status)
                }
            });

            $.ajax({
                method: "GET",
                url: "/Reportes/GetTopProductosVendidosByMes",
                data: {},
                success: function (result) {

                    var ctx = $('#topProductosVendidosMes')[0].getContext('2d');
                    var myChart = new Chart(ctx, {
                        type: 'bar',
                        data: {
                            labels: result.lista.map(p => p.NombreProducto),
                            datasets: [{
                                label: 'Cantidad de veces vendido al mes',
                                data: result.lista.map(p => p.TotalVendidos),
                                backgroundColor: 'rgba(75, 192, 192, 0.5)',
                                borderColor: 'rgba(75, 192, 192, 1)',
                                borderWidth: 2
                            }]
                        },
                        options: {
                            scales: {
                                y: {
                                    beginAtZero: true
                                }
                            }
                        }
                    });

                },
                error: function (xhr, status, error) {
                    console.log("error" + error, "no Error: " + xhr.status)
                }
            });
        });

    
/* END EXTERNAL SOURCE */
/* BEGIN EXTERNAL SOURCE */



        $(document).ready(function () {

            $.ajax({
                method: "GET",
                url: "/Reportes/ReporteCompras",
                data: {},
                success: function (result) {

                    var ctx = $('#bar')[0].getContext('2d');
                    var myChart = new Chart(ctx, {
                        type: 'bar',
                        data: {
                            labels: ['Hoy'],
                            datasets: [{
                                label: 'Compras registradas en el día',
                                data: [result.lista.length],
                                backgroundColor: 'rgba(75, 192, 192, 0.2)',
                                borderColor: 'rgba(75, 192, 192, 1)',
                                borderWidth: 1
                            }]
                        },
                        options: {
                            scales: {
                                y: {
                                    beginAtZero: true
                                }
                            }
                        }
                    });

                },
                error: function (xhr, status, error) {
                    console.log("error" + error, "no Error: " + xhr.status)
                }
            });
     /***************************************************************************************************************************************************************************************************************************************************************************************************************************************************/
        });

    
/* END EXTERNAL SOURCE */
/* BEGIN EXTERNAL SOURCE */

/* END EXTERNAL SOURCE */
/* BEGIN EXTERNAL SOURCE */



        $(document).ready(function () {

            $.ajax({
                method: "GET",
                url: "/Usuario/ReporteCompras",
                data: {},
                success: function (result) {
                    console.log(result)
                },
                error: function (xhr, status, error) {
                    console.log("error" + error, "no Error: " + xhr.status)
                }
            });
     /**************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************/
        });

    
/* END EXTERNAL SOURCE */
/* BEGIN EXTERNAL SOURCE */

/* END EXTERNAL SOURCE */
/* BEGIN EXTERNAL SOURCE */



        $(document).ready(function () {

                           

        /*$.ajax({
            method: "GET",
            url: "/Reportes/ReporteCompras",
            data: {}Typesfunction (data) {

               console.log(data);/*var ctx = $('#bar')[0].getContext('2d');
                var myChart = new Chart(ctx, {
                    type: 'line',
                    data: {
                        labels: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo'],
                        datasets: [{
                            label: 'Compras registradas en el día',
                            data: [data.lista.length],
                            backgroundColor: 'rgba(75, 192, 192, 0.2)',
                            borderColor: 'rgba(75, 192, 192, 1)',
                            borderWidth: 1
                        }]
                    },
                    options: {
                        scales: {
                            y: {
                                beginAtZero: true
                            }
                        }
                }
            });

            console.log(data)

            error: nction (xhr, status, error) {
                console.log("error" + error, "no Error: " + xhr.status)
            }
        });
        });

  
        */  
/* END EXTERNAL SOURCE */
