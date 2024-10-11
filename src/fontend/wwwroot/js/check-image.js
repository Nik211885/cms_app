function validate() {
    var fileInput = document.getElementById('avatar');
    var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.gif)$/i;
    if (fileInput.value !== '') {
        if (!allowedExtensions.exec(fileInput.value)) {
            Swal.fire({
                title: "Thông báo!",
                text: "Chỉ chấp nhận ảnh có định dạng: .jpg, .jpeg, .png, .gif. Vui lòng chọn định dạng ảnh cho phép!",
                icon: "warning",
                confirmButtonText: "Đã hiểu!"
            });
            fileInput.value = '';
            document.getElementById('avatar-input').src = '/data/img/default-avatar.jpg';
            return;
        }
    }

   /* if (fileInput.files.length > 0) {
        var fileSize = fileInput.files[0].size;
        var maxSize = 0.5 * 1024 * 1024;
        if (fileSize > maxSize) {
            Swal.fire({
                title: "Lỗi!",
                text: "Dung lượng ảnh vượt quá 500Kb. Vui lòng chọn ảnh có kích thước nhỏ hơn.",
                icon: "error",
                confirmButtonText: "Đã hiểu!"
            });
            fileInput.value = '';
            document.getElementById('avatar-input').src = '/data/img/default-avatar.jpg';
            return;
        }
    }*/

    const avatarBox = document.getElementById('avatar-input');
    const image = document.getElementById('avatar').files[0];
    const reader = new FileReader();
    reader.onloadend = function () {
        avatarBox.src = reader.result;
    }
    if (image) {
        reader.readAsDataURL(image);
    } else {
        avatarBox.src = '/data/img/default-avatar.jpg';
    }
}