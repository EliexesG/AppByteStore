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
                                },
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
                                label: 'Productos más comprados del mes',
                                data: result.lista.map(p => p.TotalVendidos),
                                backgroundColor: 'rgba(43, 94, 33, 0.5)',
                                borderColor: 'rgba(43, 94, 33 , 1)',
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
                                label: 'Vendedores con mejor evaluación ',
                                data: result.lista.map(p => p.PromedioEvaluaciones),
                                backgroundColor: 'rgba(46, 175, 249, 0.5)',
                                borderColor: 'rgba(46, 175, 249, 1)',
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
                url: "/Reportes/GetVendedoresPeorEvaluados",
                data: {},
                success: function (result) {

                    var ctx = $('#peorEvaluacion')[0].getContext('2d');
                    var myChart = new Chart(ctx, {
                        type: 'bar',
                        data: {
                            labels: result.lista.map(p => `${p.Nombre} ${p.PrimerApellido}`),
                            datasets: [{
                                label: 'Vendedores peores calificados',
                                data: result.lista.map(p => p.PromedioEvaluaciones),
                                backgroundColor: 'rgba(249, 46, 65, 0.5)',
                                borderColor: 'rgba(255, 0, 23 , 1)',
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
