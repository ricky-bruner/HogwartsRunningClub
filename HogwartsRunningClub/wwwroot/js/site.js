// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Giphy API Key 2hQWf6oBnBjUicY5yBYwgVT9ecVh5X9y

fetch(`/home/giphy?q=harry+potter`)
    .then(res => res.json())
    .then(res => {
        console.log(res)
        res.data.map(gif => {
            console.log(gif.url)
            document.querySelector("#gifs").innerHTML += `
                <img src="${gif.images.fixed_height.url}" alt="gif ${gif.title}"/>
            `
        })
    });


//var xhr = $.get("http://api.giphy.com/v1/gifs/search?q=harry+potter&api_key=2hQWf6oBnBjUicY5yBYwgVT9ecVh5X9y&limit=5");
//xhr.done(function (data) {
//    console.log("success got data", data);
//});

