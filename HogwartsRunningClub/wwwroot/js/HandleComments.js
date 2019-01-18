

//hide the forms on load. Did this way because emoji picker only renders and functions properly on elements on the DOM at load.
$("#add-comment-form-container").hide();
$(".edit-comment-container").hide();


// Hide the button, show the form when adding a comment
$("#add-comment-btn").on("click", () => {
    $("#add-comment-form-container").show();
    $("#add-comment-btn-container").hide();
})


// event listener for adding comment form
$("#comment-form").on("click", (e) => {

    // if cancel is clicked, prevent form submission and reset the value of the 'textarea', then show/hide the form and button back around
    if (e.target.id === "cancel-comment-btn") {
        e.preventDefault();
        $("#add-comment-form .emoji-wysiwyg-editor").html("");
        $("#add-comment-form-container").hide();
        $("#add-comment-btn-container").show();
    }

});

// scoped here so that comment can be retained for cancel without needing to do a fetch call
let commentContent = "";

//event listener for entire comments container to handle edits
$("#comments-container").on("click", (e) => {

    // when edit is clicked, disable the button, set the global variable with the original comment, hide content and show form
    if (e.target.id.includes("edit-comment-btn")) {

        e.target.setAttribute("disabled", true);
        let id = e.target.id.split("-")[3];

        commentContent = $(`#comment-content-${id}`).html();

        $(`#comment-content-${id}`).hide();

        $(`#edit-comment-form-${id}`).show();
    }

    // cancel is clicked, prevent submission, reactivate edit btn, reset textarea back to back to original value, and show/hide the form/content
    if (e.target.id.includes("cancel-edit-btn")) {
        e.preventDefault();

        let id = e.target.id.split("-")[3];

        $(`#edit-comment-btn-${id}`).attr("disabled", false);
        $(`#edit-comment-form-${id} .emoji-wysiwyg-editor`).html(commentContent);
        $(`#edit-comment-form-${id}`).hide();
        $(`#comment-content-${id}`).show();
    }
})

// grab all comment text from DOM
let leads = document.querySelectorAll(".emoji-convert");

// loop over, searching for emoji text
leads.forEach(lead => {
    // emoji text ex. ":smiley:"
    if (lead.textContent.includes(":")) {
        //grab original text
        let text = lead.innerHTML;
        // create array to recieve requisite emojis
        let emojis = [];
        // loop over list of emojis from emojis.js
        $.emojiarea.icons.map(grouping => {
            // grab emoji name keys only for comparison
            let keyArray = Object.keys(grouping.icons);
            // loop over this array in search of matching emojis
            keyArray.map(key => {
                // build a better emoji object
                if (text.includes(key)) {
                    let emoji = {
                        name: key,
                        img: `<img class="lead-emoji" src="/images/EmojiImages/${grouping.icons[key]}" alt="${key}">`
                    }
                    emojis.push(emoji);
                }
            })
        })

        // loop over emojis array and split the text, inserting images where text had been for each emoji
        emojis.map(emoji => {

            let splitIndex = text.split(emoji.name);

            let newText = ""

            // fixer for if more than one of same emoji
            if (splitIndex.length > 2) {

                // start off with first two index, adding first emoji img where text was
                newText = text.split(emoji.name)[0] + emoji.img + text.split(emoji.name)[1];

                // for each index after initial two, add on an img and then remaining index
                for (let i = 2; i < splitIndex.length; i++) {
                    newText += emoji.img + text.split(emoji.name)[i]
                }

            } else {

                // if not duplicating
                newText = text.split(emoji.name)[0] + emoji.img + text.split(emoji.name)[1];
            }

            // set text to equal new version with img instead of text, then move on to the next emoji is needed
            text = newText;
        })

        // replace original text with new emoji text
        lead.innerHTML = text;
    }
})

