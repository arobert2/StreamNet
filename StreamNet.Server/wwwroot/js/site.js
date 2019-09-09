// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//Change input for image upload.
$('#imageUpload').on('change', function () {
    var filename = $(this).val();
    filename = filename.substring(filename.lastIndexOf('\\') + 1);
    $(this).next('.custom-file-label').html(filename).substring();
})