
let buttons = document.querySelectorAll(".btn-sm");
let container = document.querySelector("#topic-container");
let arrayOfTopics = [];
let topics = document.querySelectorAll(".topic");
topics.forEach(t => arrayOfTopics.push(t));


buttons.forEach(button => {
    button.addEventListener("click", (e) => {
        if (e.target.id == "all-topics-btn") {
            container.innerHTML = "";
            arrayOfTopics.forEach(t => container.appendChild(t));
        } else {
            let filteredTopics = arrayOfTopics.filter(t => t.children[0].id.split("-")[2] === e.target.id.split("-")[0])
            container.innerHTML = "";
            filteredTopics.forEach(t => container.appendChild(t))
        }
    })
})

    