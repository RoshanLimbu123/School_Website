$(function () {
    $('.delete').on('click', function () {
        if (!confirm("Are you sure to delete?")) {
            event.preventDefault();
        }
    });

    $("#file").change(function () {
        readURL(this);
    });
    function readURL(input) {

        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#image').attr('src', e.target.result);
            }
            reader.readAsDataURL(input.files[0]);
        }
    }
})
