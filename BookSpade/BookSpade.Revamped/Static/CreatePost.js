var addBook = false;

//Must come after definition of the 'ul' element that we are binding to
stroll.bind('.ChooseBook ul');

function clickBookListItem() {
    $(this).siblings(".selected").removeClass("selected");
    $(this).addClass("selected");

    $("#bookTitle").removeAttr('required');
    $("#course").removeAttr('required');
    $("#isbn").removeAttr('required');

    $(".new-book").css('display', 'none');
    $(".submit-form").css('margin-bottom', '222px');
    $(".common-fields").css('margin-top', '30px');
    $("#newBookButton").css('display', 'inline-block');

    var textbookId = $(this).find("#Title")[0].getAttribute('data-value');
    $("#TextBookId").val(textbookId);
    $("#IsNewBook").val("false");
}

function clickNewBookButton() {
    addBook = true;
    $("#BookList li.selected").removeClass("selected");

    $("#bookTitle").prop("required", "true");
    $("#course").prop("required", "true");
    $("#isbn").prop("required", "true");

    $(".submit-form").css('margin-bottom', '0');
    $(".common-fields").css('margin-top', '0');
    $(".new-book").css('display', 'block');
    $(this).css('display', 'none');

    $("#TextBookId").val("");
    $("#IsNewBook").val("true");
}

function changePostType() {
    var value = $(this).val();

    if (value == "Buyer") {
        $("#matchButton").text("Match Me With a Seller!");
        $("#Price").parent().siblings('.control-label').text("I'm willing to pay (at most)");
    } else if (value == "Seller") {
        $("#matchButton").text("Match Me With a Buyer!");
        $("#Price").parent().siblings('.control-label').text("I'd like to receive (at least)");
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

// Attach event handlers
$("#BookList li").click(clickBookListItem);
$("#newBookButton").click(clickNewBookButton);
$("#ActionBy").change(changePostType);
$("#IsNegotiable").click(clickNegotiatePrice);


//    var xhr = null;
//    $("#BookSearch").keyup(function () {
//        var searchVal = $("#BookSearch").val().trim();

//        if (xhr && xhr.readyState != 4) {
//            xhr.abort();
//        }

//        xhr = $.ajax({
//            url: '@Url.Action("GetTextbooks", "Posting")',
//            data: { searchString: searchVal },
//            type: 'POST',
//            success: function (result) {
//                $("#BookList").html(result);
//            }
//        });
//    });
//</script>

//<script type="text/javascript">
//    $(document).ready(function () {
//        var newBookForm = $("#NewBookForm");
//        newBookForm.validate({
//            errorClass: "errormessage",
//            onkeyup: false,
//            onfocusout: false,
//            errorClass: 'error',
//            validClass: 'valid',
//            rules: {
//                Title: {
//                    required: true,
//                    minlength: 5,
//                    title: true
//                },
//                Author: {
//                    author: true
//                },
//                Course: {
//                    required: true,
//                    minlength: 3,
//                    maxlength: 10,
//                    course: true
//                },
//                ISBN: {
//                    required: true,
//                    minlength: 10,
//                    maxlength: 13,
//                    isbn: true
//                },
//                BookImageURL: {
//                    url: true
//                }
//            },
//            errorPlacement: function (error, element) {
//                // Set positioning based on the elements position in the form
//                var elem = $(element),
//                    corners = ['left center', 'right center'],
//                    flipIt = elem.parents('span.right').length > 0;

//                // Check we have a valid error message
//                if (!error.is(':empty')) {
//                    // Apply the tooltip only if it isn't valid
//                    elem.filter(':not(.valid)').qtip({
//                        overwrite: false,
//                        content: error,
//                        position: {
//                            my: corners[flipIt ? 0 : 1],
//                            at: corners[flipIt ? 1 : 0],
//                            viewport: $(window)
//                        },
//                        show: {
//                            event: false,
//                            ready: true
//                        },
//                        hide: false,
//                        style: {
//                            classes: 'qtip-red' // Make it red... the classic error colour!
//                        }
//                    })

//                    // If we have a tooltip on this element already, just update its content
//                    .qtip('option', 'content.text', error);
//                }

//                    // If the error is empty, remove the qTip
//                else { elem.qtip('destroy'); }
//            },
//            success: $.noop, // Odd workaround for errorPlacement not firing!
//        })
//    });

//    function ValidateFields() {
//        var newBookForm = $("#NewBookForm");
//        return newBookForm.validate().form();
//    }
//</script>


//@section scripts {
//    <script src="@Url.Content("~/Scripts/ToolTip.js")" type="text/javascript"></script>
//    <script type="text/javascript">
//        $(function () {
//            $(":radio[name=Pref]").change(function () {
//                if ($(this).val() == 'True') {
//                    $("#divContactEmail").show(); 
//                } else {
//                    $("#divContactEmail").val("");
//                    $("#divContactEmail").hide();
//                }
//            });

//            $("#SubmitButton").click(function () {
//                if (ValidateFields()) {
//                    if (!$("li.selected").length && !addBook) {
//                        ErrorDialog();
//                    } else {
//                        ContactPrefDialog();
//                    }
//                }
//            });
//        });


//    function CommonFields() {
//        var isBuy = $("#PostType").val() == "Buy";
//        var price = $("#Price").val().trim();
//        var condition = $("#Condition").val().trim();
//        var email = $("#txtContactEmail").val();
//        var IsNegotiable = $('#cbNegotiationPrice').is(':checked')

//        var fields = {
//            "isBuy": isBuy,
//            "price": price,
//            "condition": condition,
//            "email": email,
//            "IsNegotiable": IsNegotiable
//        }
//        return fields;
//    }

//    function ErrorDialog() {
//        $("#ErrorDialog").dialog({
//            height: 200,
//            width: 500,
//            modal: true,
//            buttons: {
//                "OK" : function () {
//                    $(this).dialog("close"); 
//                }
//            }
//        }); 
//    }

//    function AddPost() {
//        var course = $("li.selected #Course").text().trim();
//        var title = $("li.selected #Title").text().trim();
//        var common = CommonFields(); 

//        $.ajax({
//            url: '@Url.Action("SavePost", "Posting")',
//            data: {
//                profileID: 0,
//                course : course,
//                title : title,
//                isBuy : common.isBuy,
//                price : common.price,
//                condition : common.condition,
//                email: common.email,
//                IsNegotiable: common.IsNegotiable
//            },
//            type: 'POST',
//            success: function (result) {
                   
//                var link = "@(Url.Action("Index", "Posting", new {postID = 1}))"
//                document.location.href = link.replace(1, result);
//            }
//        });
//    }

//    function NewBook() {
//        var title = $(".NewBook #Title").val().trim();
//        var author = $(".NewBook #Author").val().trim();
//        var course = $(".NewBook #Course").val().trim();
//        var isbn = $(".NewBook #ISBN").val().trim();
//        var bookImageURL = $(".NewBook #BookImageURL").val().trim();
//        var common = CommonFields();
//        $.ajax({
//            url: '@Url.Action("SaveBook", "Posting")',
//            data: {
//                isbn : isbn,
//                title : title,
//                author : author,
//                course : course,
//                bookImageURL : bookImageURL,
//                isBuy : common.isBuy,
//                price : common.price,
//                condition : common.condition,
//                email: common.email,
//                IsNegotiable: common.IsNegotiable
//            },
//            type: 'POST',
//            success: function (result) {
//                var link = "@(Url.Action("Index", "Posting", new {postID = 1}))"
//                document.location.href = link.replace(1, result); 
//            }
//        });
//    }
//</script>

//<script type="text/javascript">
//    $(function () {
//        $(".AddPost").tooltip();
//        $(".PostType").tooltip();
//    });

//// Validation Checks
        
//function FilterInputByDigits(event) {
//    var keyCode = ('which' in event) ? event.which : event.keyCode;

//    isNumeric = (keyCode >= 48 /* KeyboardEvent.DOM_VK_0 */ && keyCode <= 57 /* KeyboardEvent.DOM_VK_9 */) ||
//                (keyCode >= 96 /* KeyboardEvent.DOM_VK_NUMPAD0 */ && keyCode <= 105 /* KeyboardEvent.DOM_VK_NUMPAD9 */);

//    // keyCode = 8 => backspace, keyCode = 46 => delete
//    modifiers = (event.altKey || event.ctrlKey || event.shiftKey || keyCode == 8 || keyCode == 46);

//    return isNumeric || modifiers;
//}
//</script>