// chart_type -> zmienna z sekcji <script> html

var options = {
    chart: {
      type: chart_type
    },
    series: [32, 2, 0],
    labels: ['Win', 'Lose', 'Draw']
} 
var chart = new ApexCharts(document.querySelector("#chart"), options);

chart.render();