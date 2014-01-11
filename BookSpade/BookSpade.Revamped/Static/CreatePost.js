var addBook = false;

//Must come after definition of the 'ul' element that we are binding to
stroll.bind('.ChooseBook ul');

var createPostForm = $("#createPostForm");

$(document).ready(function () {
    // existing textbook id is initialized to 0 by default
    // so unset it
    $("#TextBookId").val("");

    createPostForm.validate({
        onfocusout: function(element) { $(element).valid(); },
        errorClass: 'error',
        validClass: 'valid',
        rules: {
            Price: {
                required  : true,
                maxlength : 9
            },
            TextBookId: {
                required: true
            }
        },
        messages: {
            TextBookId: {
                required: jQuery.format("Please choose your book")
            }
        },
        ignore: [],
        errorPlacement: function (error, element) {
            // Set positioning based on the elements position in the form
            var elem = $(element),
                corners = ['left center', 'right center'];

            // Hacky solution for validating existing book selection
            var dest = elem,
                existingBook = elem[0].id == "TextBookId";
            
            if (existingBook) {
                dest = $("#BookList");
                corners = ['right center', 'left center'];
            }

            // Check we have a valid error message
            if (!error.is(':empty')) {
                // Apply the tooltip only if it isn't valid
                elem.filter(':not(.valid)').qtip({
                    overwrite: false,
                    content: error,
                    position: {
                        my: corners[0],
                        at: corners[1],
                        target: dest,
                        viewport: $(window)
                    },
                    show: {
                        event: false,
                        ready: true
                    },
                    hide: false,
                    style: {
                        classes: 'qtip-red' // Make it red... the classic error colour!
                    }
                })

                // If we have a tooltip on this element already, just update its content
                .qtip('option', 'content.text', error);
            }
            // If the error is empty, remove the qTip
            else { elem.qtip('destroy'); }
        },
        success: $.noop, // Odd workaround for errorPlacement not firing!
    });
});

function removeValidationsForNewBook() {
    $("#BookTitle").rules("remove");
    $("#CourseName").rules("remove");
    $("#ISBN").rules("remove");

    $('#BookTitle').qtip('destroy');
    $('#CourseName').qtip('destroy');
    $('#ISBN').qtip('destroy');
}

function addValidationsForNewBook() {
    $("#BookTitle").rules("add", {
        required: true,
        minlength: 2,
        messages: {
            minlength: jQuery.format("Please enter at least {0} characters")
        }
    });

    $("#CourseName").rules("add", {
        required: true,
        minlength: 2,
        messages: {
            minlength: jQuery.format("Please enter at least {0} characters")
        }
    });

    $("#ISBN").rules("add", {
        required: true,
        minlength: 9,
        messages: {
            minlength: jQuery.format("Please enter at least {0} characters")
        }
    });
}

var list = $("#BookList li");
var arr = $.makeArray(list.map(function (k, v) {
    return $(v).text().toLowerCase();
}));

function searchBookList() {
    var userInput = $(this).val().toLowerCase();
    list.each(function (index, value) {
        $(value).toggle(arr[index].indexOf(userInput) >= 0);
    });
}

function clickBookListItem() {
    $(this).siblings(".selected").removeClass("selected");
    $(this).addClass("selected");

    removeValidationsForNewBook();
    $('#Price').qtip('destroy');

    $(".new-book").css('display', 'none');
    $(".submit-form").css('margin-bottom', '222px');
    $(".common-fields").css('margin-top', '30px');

    var textbookId = $(this).find("#Title")[0].getAttribute('data-value');
    $("#TextBookId").val(textbookId);
    $("#IsNewBook").val("false");

    $("#TextBookId").rules("add", {
        required: true,
        messages: {
            required: jQuery.format("Please choose your book")
        }
    });

    $("#newBookButton").fadeIn();
}

function clickNewBookButton() {
    addBook = true;
    $("#BookList li.selected").removeClass("selected");

    addValidationsForNewBook();
    $('#Price').qtip('destroy');

    $(".submit-form").css('margin-bottom', '0');
    $(".common-fields").css('margin-top', '0');
    $(".new-book").slideDown();

    $("#TextBookId").val("");
    $("#IsNewBook").val("true");

    $("#TextBookId").rules("remove");
    $('#TextBookId').qtip('destroy');

    $(this).fadeOut();
}

function changePostType() {
    var value = $(this).val();

    if (value == "Buyer") {
        $("#matchButton").text("Match Me With a Seller!");
        $("#Price").parent().parent().siblings('.control-label').text("I'm willing to pay (at most)");
    } else if (value == "Seller") {
        $("#matchButton").text("Match Me With a Buyer!");
        $("#Price").parent().parent().siblings('.control-label').text("I'd like to receive (at least)");
    }
}

function clickNegotiatePrice() {
    var checked = $(this).is(':checked');

    if (checked) {
        $("#Price").val("");
        $("#Price").prop('disabled', true);
    } else {
        $("#Price").prop('disabled', false);
    }
}

$(function () {
    // Attach event handlers
    $("#BookSearch").keyup(searchBookList);
    $("#BookList li").click(clickBookListItem);
    $("#newBookButton").click(clickNewBookButton);
    $("#ActionBy").change(changePostType);
    $("#IsNegotiable").click(clickNegotiatePrice);
});
