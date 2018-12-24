console.log("Hello, World")

document.querySelector("#add-comment-btn").addEventListener("click", (e) => {
    document.querySelector("#comment-form").innerHTML = `
        <form>
            <textarea placeholder="Keep is civil, make Dumbledore proud..."></textarea>
            <button>Send</button>
        </form>
    `
});

document.querySelector("#show-comments").addEventListener("click", (e) => {
    fetch("http://localhost:5000/comments/getcomments")
        .then(res => res.json())
        .then(comments => {
            console.log(comments)
            comments.map(c => {
                document.querySelector("#comments-container").innerHTML += `
                    <div>
                        <p>${c.content}</p>
                    </div>
                `
            })
        })
})