console.log("Hello, World")

document.querySelector("#comment-form").addEventListener("click", (e) => {
    
    if (e.target.id === "add-comment-btn") {
        let topicId = document.querySelector(".topic-card").id.split("-")[1];
        document.querySelector("#comment-form").innerHTML = `
            <form action="/Comments/CreateComment?topicId=${topicId}" method="post">
                <div class="form-group">
                    <textarea class="form-control" placeholder="Keep is civil, make Dumbledore proud..." rows="5" id="comment-content" name="Content"></textarea>
                </div>
                <div class="d-flex justify-content-center">
                    <button class="col-md-1 btn btn-sm btn-primary mr-2" id="submit-comment-btn">Send</button>
                    <button class="col-md-1 btn btn-sm btn-danger mr-2" id="cancel-comment-btn">Cancel</button>
                </div>
            </form>
        `
    }

    if (e.target.id === "cancel-comment-btn") {
        e.preventDefault();
        document.querySelector("#comment-form").innerHTML = `
            <div class="form-group row justify-content-md-center">
                <button class="btn btn-sm btn-primary" id="add-comment-btn">Comment</button>
            </div>    
        `
    }

});

document.querySelector("#comments-container").addEventListener("click", (e) => {
    if (e.target.id.includes("edit-comment-btn")) {
        let id = e.target.id.split("-")[3];
        e.target.setAttribute("disabled", true);
        let contentElement = document.querySelector(`#comment-content-${id}`);
        let content = contentElement.textContent;
        contentElement.innerHTML = `
            <form action="/Comments/EditComment?CommentId=${id}" method="post">
                <textarea name="Content" class="form-control" rows="5">${content}</textarea>
                <div class="d-flex justify-content-center">
                    <button class="btn btn-sm btn-secondary mt-2 mr-2" id="cancel-edit-btn-${id}">Cancel</button>
                    <button class="btn btn-sm btn-warning mt-2">Save Changes</button>
                </div>
            </form>`;
    }

    if (e.target.id.includes("cancel-edit-btn")) {
        e.preventDefault();
        let id = e.target.id.split("-")[3];
        let contentElement = document.querySelector(`#comment-content-${id}`);
        let remoteURL = "http://localhost:5000";
        console.log(`${remoteURL}/comments/getcomment/${id}`)
        fetch(`${remoteURL}/comments/getcomment/${id}`)
            .then(res => res.json())
            .then(res => {
                contentElement.innerHTML = "";
                contentElement.textContent = res.content;
                document.querySelector(`#edit-comment-btn-${id}`).disabled = false;
            })
    }
})