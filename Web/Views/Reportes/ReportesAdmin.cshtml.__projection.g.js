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
     /*******************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************/
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
