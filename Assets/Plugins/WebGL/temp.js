var ImageUploaderPlugin = {
  ImageUploaderCaptureClick: function() {
    if (!document.getElementById('ImageUploaderInput')) {
      var fileInput = document.createElement('input');
      fileInput.setAttribute('type', 'file');
      fileInput.setAttribute('id', 'ImageUploaderInput');
      fileInput.setAttribute('multiple', '');
      fileInput.setAttribute('accept', 'png, jpg');
      // fileInput.style.visibility = 'hidden';
      fileInput.onchange = function(event) {
        for (let index = 0; index < event.target.files.length; index++) {
          const file = event.target.files[index];
          SendMessage('Canvas', 'FileSelected', URL.createObjectURL(file))
        }
      }
      document.body.appendChild(fileInput);
    }
    /*
    const OpenFileDialog = function() {
      document.getElementById('ImageUploaderInput').click();
      document.getElementById('unity-canvas').removeEventListener('click', OpenFileDialog);
    };
    document.getElementById('unity-canvas').addEventListener('click', OpenFileDialog, false);
    */
  }
};
mergeInto(LibraryManager.library, ImageUploaderPlugin);