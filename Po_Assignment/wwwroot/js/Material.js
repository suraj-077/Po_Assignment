$("#saveButton").click(function (e) {
    e.preventDefault(); // Prevent the default form submission
    var formValid = true;

    // Check all input, select, and textarea fields for empty values
    $("#materialForm input[type='text'],  input[type='number'], #materialForm textarea").each(function () {
        if ($(this).val().trim() === "") {
            formValid = false;
            return false; // Exit the loop early if any field is empty
        }
    });

    if (!formValid) {
        alert("Please fill out all required fields.");
        return; // Exit the function if any field is empty
    }
    var isValid1 = true
    var isValid2 = true

    function ValidateText(input) {
        var errorMessageSpan = document.getElementById("textError");
        var text = input.value.trim();
        var regex = /^[A-Za-z]+$/; // Regular expression to match only characters
        if (!regex.test(text)) {
            errorMessageSpan.innerText = "Please enter only characters.";
            isValid1 = false
        } else {
            errorMessageSpan.innerText = ""; // Clear the error message if input is valid
            isValid1 = true
        }
    }


    function ValidateText2(input) {
        var errorMessageSpan = document.getElementById("textError2");
        var text = input.value.trim();
        var regex = /^[A-Za-z]+$/; // Regular expression to match only characters
        if (!regex.test(text)) {
            errorMessageSpan.innerText = "Please enter only characters.";
            isValid2 = false
        } else {
            errorMessageSpan.innerText = ""; // Clear the error message if input is valid
            isValid2 = true
        }
    }
    

    var materialData = {
        Code: $("#code").val(),
        ShortText: $("#ShortText").val(),
        LongText: $("#LongText").val(),
        Reorder_Level: $("#Reorder_Level").val(),
        Min_Order_Quantity: $("#Min_Order_Quantity").val(),
        Unit: $("#Unit").val(),
        IsActive: $("#IsActive").is(":checked")
    };

    $.ajax({
        type: 'POST',
        url: "/MaterialEntry/AddMaterial",
        data: materialData,
        success: function (response) {
            Swal.fire({
                icon: 'success',
                title: 'Material details updated Successfully',
                showConfirmButton: false,
                timer: 2000 // Close the alert after 3 seconds
            }).then(function () {
                // Redirect to the index page or perform any other action
                window.location.href = "/MaterialEntry/Index";
            });

           // alert('Material data saved successfully');
            // Redirect to the specified URL
        },
        error: function (xhr, status, error) {
            console.error(error);
            // Handle error
        }
    });
});


//$("#saveButton").click(function (e) {
//    e.preventDefault(); // Prevent the default form submission
//    var formValid = true;

//    // Check all input, select, and textarea fields for empty values
//    $("#materialForm input[type='text'], input[type='number'], #materialForm textarea").each(function () {
//        if ($(this).val().trim() === "") {
//            formValid = false;
//            return false; // Exit the loop early if any field is empty
//        }
//    });

//    if (!formValid) {
//        alert("Please fill out all required fields.");
//        return; // Exit the function if any field is empty
//    }

//    var formData = $("#materialForm").serialize();


//    if (isValid1 && isValid2) {
//        // Collect material data

//        $.ajax({
//            type: 'POST',
//            url: "/MaterialEntry/Edit",
//            data: formData,
//            success: function (response) {
//                Swal.fire({
//                    icon: 'success',
//                    title: 'Material data Added Successfully',
//                    showConfirmButton: false,
//                    timer: 2000 // Close the alert after 2 seconds
//                }).then(function () {
//                    // Redirect to the index page or perform any other action
//                    window.location.href = '/MaterialEntry/Index';
//                });
//                // Redirect to the specified URL
//            },
//            error: function (xhr, status, error) {
//                console.error(error);
//                // Handle error
//            }
//        });

//    }
//}