const ImageUploaderPlugin = {
  ImageUploaderCaptureClick: function() {
    if (!document.getElementById('ImageUploaderInput')) {
      const fileInput = document.createElement('input');
      fileInput.setAttribute('type', 'file');
      fileInput.setAttribute('id', 'ImageUploaderInput');
      fileInput.setAttribute('multiple', '');
      fileInput.setAttribute('accept', 'png, jpg');
      fileInput.onchange = (event) => {
        for (const file of event.target.files) {
          SendMessage('Canvas', 'FileSelected', URL.createObjectURL(file))
        }
      };
      document.body.appendChild(fileInput);
    }
  }
};
mergeInto(LibraryManager.library, ImageUploaderPlugin);