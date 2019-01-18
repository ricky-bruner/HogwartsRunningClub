

$("#add-comment-form-container").hide();
$(".edit-comment-container").hide();

$("#add-comment-btn").on("click", () => {
    $("#add-comment-form-container").show();
    $("#add-comment-btn-container").hide();
})





$("#comment-form").on("click", (e) => {

    if (e.target.id === "cancel-comment-btn") {
        e.preventDefault();
        $("#add-comment-form .emoji-wysiwyg-editor").html("");
        $("#add-comment-form-container").hide();
        $("#add-comment-btn-container").show();
    }



    //if (e.target.id === "add-comment-btn") {
    //    let house = e.target.classList[2].split("-")[1];
    //    let commentBtnContainer = document.querySelector("#add-comment-btn-container");
    //    let addCommentBtn = e.target.parentElement;
    //    commentBtnContainer.removeChild(addCommentBtn);
    //    document.querySelector("#add-comment-form").innerHTML += `
    //            <div class="form-group">
    //                <textarea class="form-control emojis-wysiwyg" placeholder="Keep is civil, make Dumbledore proud..." rows="5" id="comment-content" name="Content"></textarea>
                    
    //            </div>
    //            <div class="d-flex justify-content-center">
    //                <button class="col-md-1 btn btn-sm btn-${house} mr-2" id="submit-comment-btn">Send</button>
    //                <button class="col-md-1 btn btn-sm btn-secondary mr-2" id="cancel-comment-btn">Cancel</button>
    //            </div>
    //    `

    //    let textDiv = document.createElement("div");
    //    textDiv.setAttribute("class", "form-group");

    //    let textarea = document.createElement("textarea");
    //    textarea.setAttribute("class", "form-control emojis-wysiwyg");
    //    textarea.setAttribute("placeholder", "Keep is civil, make Dumbledore proud...");
    //    textarea.setAttribute("rows", 5);
    //    textarea.setAttribute("id", "comment-content");
    //    textarea.setAttribute("name", "Content");

    //    textDiv.appendChild(textarea);

    //    let buttonsDiv = document.createElement("div");
    //    buttonsDiv.setAttribute("class", "d-flex justify-content-center");

    //    let sendBtn = document.createElement("button");
    //    sendBtn.setAttribute("class", `col-md-1 btn btn-sm btn-${house} mr-2`);
    //    sendBtn.setAttribute("id", "submit-comment-btn");
    //    sendBtn.textContent = "Send";

    //    let cancelBtn = document.createElement("button");
    //    cancelBtn.setAttribute("class", "col-md-1 btn btn-sm btn-secondary mr-2");
    //    cancelBtn.setAttribute("id", "cancel-comment-btn");
    //    cancelBtn.textContent = "Cancel";

    //    buttonsDiv.appendChild(sendBtn);
    //    buttonsDiv.appendChild(cancelBtn);

    //    document.querySelector("#add-comment-form").appendChild(textDiv);
    //    document.querySelector("#add-comment-form").appendChild(buttonsDiv);

    //}

    //if (e.target.id === "cancel-comment-btn") {
    //    e.preventDefault();
    //    let house = document.querySelector("#submit-comment-btn").classList[3].split("-")[1];
    //    let form = document.querySelector("#add-comment-form")
    //    let antiForgeryInput = form.children[0];
    //    form.innerHTML = "";
    //    form.appendChild(antiForgeryInput);
    //    document.querySelector("#add-comment-btn-container").innerHTML = `
    //        <div class="form-group row justify-content-md-center">
    //            <button class="btn btn-sm btn-${house}" id="add-comment-btn">Comment</button>
    //        </div>    
    //    `
    //}

});

let commentContent = "";



$("#comments-container").on("click", (e) => {

    if (e.target.id.includes("edit-comment-btn")) {

        e.target.setAttribute("disabled", true);
        let id = e.target.id.split("-")[3];

        commentContent = $(`#comment-content-${id}`).html();

        $(`#comment-content-${id}`).hide();

        $(`#edit-comment-form-${id}`).show();
    }

    //if (e.target.id.includes("edit-comment-btn")) {
    //    let id = e.target.id.split("-")[3];
    //    let house = "";
    //    if (document.querySelector(`#add-comment-btn`) !== null) {
    //        house = document.querySelector(`#add-comment-btn`).classList[2].split("-")[1];
    //    } else {
    //        house = document.querySelector(`#submit-comment-btn`).classList[3].split("-")[1]
    //    }
    //    e.target.setAttribute("disabled", true);
    //    let contentElement = document.querySelector(`#comment-content-${id}`);
    //    let content = contentElement.innerHTML;

    //    contentElement.textContent = "";
    //    document.querySelector(`#edit-comment-form-${id}`).innerHTML += `
    //        <div class="example">
    //            <textarea name="Content" class="form-control emojis-wysiwyg" rows="5">${content}</textarea>
    //        </div>
    //        <div class="d-flex justify-content-center">
    //            <button class="btn btn-sm btn-${house} mt-2 mr-2">Save Changes</button>
    //            <button class="btn btn-sm btn-secondary mt-2 mr-2" id="cancel-edit-btn-${id}">Cancel</button>
    //        </div>

    //    `;
    //}

    if (e.target.id.includes("cancel-edit-btn")) {
        e.preventDefault();

        let id = e.target.id.split("-")[3];

        $(`#edit-comment-btn-${id}`).attr("disabled", false);
        $(`#edit-comment-form-${id} .emoji-wysiwyg-editor`).html(commentContent);
        $(`#edit-comment-form-${id}`).hide();
        $(`#comment-content-${id}`).show();
        //let contentElement = document.querySelector(`#comment-content-${id}`);
        //let editForm = document.querySelector(`#edit-comment-form-${id}`);
        //let antiForgeryInput = editForm.children[0];
        //let remoteURL = "http://localhost:5000";
        //fetch(`${remoteURL}/comments/getcomment/${id}`)
        //    .then(res => res.json())
        //    .then(res => {
        //        editForm.innerHTML = "";
        //        editForm.appendChild(antiForgeryInput);
        //        contentElement.textContent = res.content;
        //        document.querySelector(`#edit-comment-btn-${id}`).disabled = false;
        //    })
    }
})


let leads = document.querySelectorAll(".emoji-convert");

leads.forEach(lead => {
    if (lead.textContent.includes(":")) {
        let text = lead.innerHTML;
        let emojis = [];
        $.emojiarea.icons.map(grouping => {
            let keyArray = Object.keys(grouping.icons);
            keyArray.map(key => {
                if (text.includes(key)) {
                    let emoji = {
                        name: key,
                        img: `<img class="lead-emoji" src="/images/EmojiImages/${grouping.icons[key]}" alt="${key}">`
                    }
                    emojis.push(emoji);
                }
            })
        })

        emojis.map(emoji => {
            let splitIndex = text.split(emoji.name);
            console.log(splitIndex)
            let newText = ""
            if (splitIndex.length > 2) {
                newText = text.split(emoji.name)[0] + emoji.img + text.split(emoji.name)[1];
                for (let i = 2; i < splitIndex.length; i++) {
                    if (i === splitIndex.length - 1) {
                        newText += emoji.img + text.split(emoji.name)[i]
                    } else {
                        newText += emoji.img + text.split(emoji.name)[i]
                    }
                }
            } else {
                newText = text.split(emoji.name)[0] + emoji.img + text.split(emoji.name)[1];
            }
            text = newText;
        })
        lead.innerHTML = text;
    }
})

