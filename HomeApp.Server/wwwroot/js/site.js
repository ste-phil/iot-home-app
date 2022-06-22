// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

let backgroundColors =  [
    'rgba(255, 99, 132, 0.2)',
    'rgba(54, 162, 235, 0.2)',
    'rgba(255, 206, 86, 0.2)',
    'rgba(75, 192, 192, 0.2)',
    'rgba(153, 102, 255, 0.2)',
    'rgba(255, 159, 64, 0.2)'
]

let borderColors = [
    'rgba(255, 99, 132, 1)',
    'rgba(54, 162, 235, 1)',
    'rgba(255, 206, 86, 1)',
    'rgba(75, 192, 192, 1)',
    'rgba(153, 102, 255, 1)',
    'rgba(255, 159, 64, 1)'
]

let defaultDataset = {
    fill: false,
    tension: 0.1,
    borderWidth: 1
}

function plotLineChart(canvas, name, x, y) {
    const ctx = canvas.getContext('2d');
    const myChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: x,
            datasets: [{
                label: name,
                data: y,
                fill: false,
                tension: 0.1,
                backgroundColor: [
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                    'rgba(255, 159, 64, 0.2)'
                ],
                borderColor: [
                    'rgba(255, 99, 132, 1)',
                    // 'rgba(54, 162, 235, 1)',
                    // 'rgba(255, 206, 86, 1)',
                    // 'rgba(75, 192, 192, 1)',
                    // 'rgba(153, 102, 255, 1)',
                    // 'rgba(255, 159, 64, 1)'
                ],
                borderWidth: 1
            }]
        },
        options: {
        }
    });
}

function plotLineChartDatasets(canvas, datasets) {
    const ctx = canvas.getContext('2d');

    let resultSets = []
    for (let i = 0; i < datasets.length; i++) {
        Object.assign(
            datasets[i], 
            {
                backgroundColor: backgroundColors[i],
                borderColor: borderColors[i],
            },
            defaultDataset
        )
        debugger;
        resultSets.push(datasets[i])
    }

    const myChart = new Chart(ctx, {
        type: 'line',
        data: {
            datasets: resultSets
        },
        options: {
        }
    });

    return myChart
}

// plotLineChart(document.getElementById('plot-test'), "Temperatures", [1, 2, 3, 4, 5], [12, 19, 3, 5, 2, 3])