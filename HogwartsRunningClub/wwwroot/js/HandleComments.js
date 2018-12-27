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
                    <button class="col-md-1 btn btn-md btn-primary form-group" id="submit-comment-btn">Send</button>
                    <button class="col-md-1 btn btn-md btn-danger form-group" id="cancel-comment-btn">Cancel</button>
                </div>
            </form>
        `
    }

    if (e.target.id === "cancel-comment-btn") {
        e.preventDefault();
        document.querySelector("#comment-form").innerHTML = `
            <div class="form-group row justify-content-md-center">
                <button class="btn btn-md btn-primary" id="add-comment-btn">Add Comment</button>
            </div>    
        `
    }

});
