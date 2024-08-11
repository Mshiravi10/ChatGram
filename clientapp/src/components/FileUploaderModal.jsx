import React, { useState, useEffect } from "react";
import Modal from "react-modal";
import Cropper from "react-cropper";
import "cropperjs/dist/cropper.css";
import { toast } from "react-toastify";

const customStyles = {
  content: {
    top: "50%",
    left: "50%",
    right: "auto",
    bottom: "auto",
    marginRight: "-50%",
    transform: "translate(-50%, -50%)",
    width: "60%",
    maxHeight: "80%",
    backgroundColor: "#f0f0f0",
    borderRadius: "10px",
    padding: "20px",
    boxShadow: "0px 0px 15px rgba(0, 0, 0, 0.2)"
  },
};

Modal.setAppElement('#root');

const FileUploaderModal = ({ isOpen, onClose, onCrop, image }) => {
  const [cropper, setCropper] = useState();

  useEffect(() => {
    if (cropper && image) {
      cropper.replace(image);
    }
  }, [image, cropper]);

  const handleCrop = () => {
    if (cropper) {
      cropper.getCroppedCanvas().toBlob((blob) => {
        onCrop(blob);
        onClose();
      });
    } else {
      toast.error("Please crop the image.");
    }
  };

  return (
    <Modal
      isOpen={isOpen}
      onRequestClose={onClose}
      style={customStyles}
      contentLabel="Crop Image"
    >
      <h2 style={{ textAlign: "center" }}>Crop your image</h2>
      {image ? (
        <Cropper
          style={{ height: "400px", width: "100%" }} // تنظیم اندازه کراپر
          initialAspectRatio={1}
          aspectRatio={1}
          guides={false}
          viewMode={1}
          minContainerWidth={300} // حداقل عرض کانتینر
          minContainerHeight={300} // حداقل ارتفاع کانتینر
          zoomable={true} // فعال کردن زوم
          scalable={false} // غیرفعال کردن تغییر اندازه کراپر
          onInitialized={(instance) => setCropper(instance)}
        />
      ) : (
        <p>No image selected</p>
      )}
      <div style={{ display: "flex", justifyContent: "space-between", marginTop: "10px" }}>
        <button onClick={handleCrop} style={{ padding: "10px 20px", backgroundColor: "#007BFF", color: "#fff", border: "none", borderRadius: "5px", cursor: "pointer" }}>
          Crop Image
        </button>
        <button onClick={onClose} style={{ padding: "10px 20px", backgroundColor: "#6c757d", color: "#fff", border: "none", borderRadius: "5px", cursor: "pointer" }}>
          Cancel
        </button>
      </div>
    </Modal>
  );
};

export default FileUploaderModal;
