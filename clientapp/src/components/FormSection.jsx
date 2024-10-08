import React, { useState } from "react";
import { useFormik } from "formik";
import { useDropzone } from "react-dropzone";
import FileUploaderModal from "./FileUploaderModal";
import { useNavigate } from 'react-router-dom';
import axios from "axios";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import './styles/FormSection.css';

function FormSection() {
  const [file, setFile] = useState(null);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [croppedImage, setCroppedImage] = useState(null);
  const [originalFile, setOriginalFile] = useState(null);
  const [profileId, setProfileId] = useState(null);
  const navigate = useNavigate();

  const { getRootProps, getInputProps } = useDropzone({
    accept: "image/jpeg, image/png",
    onDrop: (acceptedFiles, fileRejections) => {
      if (fileRejections.length > 0) {
        fileRejections.forEach(() => {
          toast.error("Invalid file type. Only JPG and PNG are allowed.");
        });
        return;
      }
      const selectedFile = acceptedFiles[0];
      setOriginalFile(selectedFile);
      setFile(URL.createObjectURL(selectedFile));
      setIsModalOpen(true);
    },
  });

  const handleCrop = async (blob) => {
    const url = URL.createObjectURL(blob);
    setCroppedImage(url);
    const sessionId = await handleUpload(originalFile, blob);
    if (sessionId) {
      setProfileId(sessionId);
    }
  };

  const handleUpload = async (file, blob) => {
    try {
      const chunkSize = blob.size;
      const totalSize = blob.size;
      const fileName = file.name;

      const sessionId = await startUploadSession(fileName, totalSize, chunkSize);

      await uploadChunk(sessionId, 0, blob);

      await completeUploadSession(sessionId);

      toast.success("Upload successful!");
      return sessionId; 
    } catch (error) {
      toast.error("Upload failed.");
      return null;
    }
  };

  const startUploadSession = async (fileName, totalSize, chunkSize) => {
    try {
      const response = await axios.post("http://localhost:8920/api/fileupload/start-upload", {
        fileName,
        chunkSize,
        totalSize,
      });
      return response.data.sessionId;
    } catch (error) {
      toast.error("Failed to start upload session");
      throw error;
    }
  };

  const uploadChunk = async (sessionId, chunkIndex, chunkFile) => {
    try {
      const formData = new FormData();
      formData.append("SessionId", sessionId);
      formData.append("ChunkIndex", chunkIndex);
      formData.append("ChunkFile", chunkFile);

      await axios.post("http://localhost:8920/api/fileupload/upload-chunk", formData);
    } catch (error) {
      toast.error(`Failed to upload chunk ${chunkIndex}`);
      throw error;
    }
  };

  const completeUploadSession = async (sessionId) => {
    try {
      await axios.post("http://localhost:8920/api/fileupload/complete-upload", {
        sessionId,
      });
    } catch (error) {
      toast.error("Failed to complete upload session");
      throw error;
    }
  };

  const formik = useFormik({
    initialValues: {
      firstName: "",
      lastName: "",
      email: "",
      userName: "", // New UserName field
      password: "",
      confirmPassword: "",
    },
    validate: (values) => {
      const errors = {};
      if (!values.firstName) {
        errors.firstName = "First Name cannot be empty";
      } else if (values.firstName.length > 15) {
        errors.firstName = "Must be 15 characters or less";
      }

      if (!values.lastName) {
        errors.lastName = "Last Name cannot be empty";
      } else if (values.lastName.length > 20) {
        errors.lastName = "Must be 20 characters or less";
      }

      if (!values.email) {
        errors.email = "Email is required";
      } else if (
        !/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i.test(values.email)
      ) {
        errors.email = "Invalid email address";
      }

      if (!values.userName) {
        errors.userName = "User Name is required"; // Validation for UserName
      } else if (values.userName.length < 5 || values.userName.length > 15) {
        errors.userName = "User Name must be between 5 and 15 characters";
      }

      if (!values.password) {
        errors.password = "Password is required";
      } else if (values.password.length < 8) {
        errors.password = "Password must not be less than 8 characters";
      }

      if (!values.confirmPassword) {
        errors.confirmPassword = "Confirm Password is required";
      } else if (values.confirmPassword !== values.password) {
        errors.confirmPassword = "Passwords must match";
      }

      return errors;
    },
    onSubmit: async (values) => {
      try {
        const command = {
          ...values,
          profileId: profileId ? profileId : null,
        };
    
        const response = await axios.post("http://localhost:8920/api/user/register", command);
    
        if (response.data.success) {
          toast.success(response.data.message || "Registration successful!");
          navigate('/login');
        } else {
          toast.error(response.data.message || "Registration failed.");
        }
      } catch (error) {
        toast.error("Registration failed due to an error.");
      }
    },
    
  });

  return (
    <div className="section-container">
      <ToastContainer />
      <button className="trial-btn text-white cursor-pointer">
        <span className="text-bold">Chat all you want</span> – It's cheaper than texting your ex!
      </button>
      <div className="form-container">
        <div className="upload-container" {...getRootProps()}>
          <input {...getInputProps()} />
          <label className={`upload-label ${croppedImage ? 'image-uploaded' : ''}`}>
            {croppedImage ? (
              <img src={croppedImage} alt="Cropped" />
            ) : (
              <span></span>
            )}
          </label>
        </div>
        <form onSubmit={formik.handleSubmit}>
          <input
            type="text"
            placeholder="First Name"
            name="firstName"
            id="firstName"
            onChange={formik.handleChange}
            value={formik.values.firstName}
          />
          {formik.errors.firstName && <div className="error">{formik.errors.firstName}</div>}
          <input
            type="text"
            placeholder="Last Name"
            name="lastName"
            id="lastName"
            onChange={formik.handleChange}
            value={formik.values.lastName}
          />
          {formik.errors.lastName && <div className="error">{formik.errors.lastName}</div>}
          <input
            type="email"
            placeholder="Email Address"
            name="email"
            id="email"
            onChange={formik.handleChange}
            value={formik.values.email}
          />
          {formik.errors.email && <div className="error">{formik.errors.email}</div>}
          <input
            type="text"
            placeholder="User Name" // New UserName input field
            name="userName"
            id="userName"
            onChange={formik.handleChange}
            value={formik.values.userName}
          />
          {formik.errors.userName && <div className="error">{formik.errors.userName}</div>}
          <input
            type="password"
            placeholder="Password"
            name="password"
            id="password"
            onChange={formik.handleChange}
            value={formik.values.password}
          />
          {formik.errors.password && <div className="error">{formik.errors.password}</div>}
          <input
            type="password"
            placeholder="Confirm Password"
            name="confirmPassword"
            id="confirmPassword"
            onChange={formik.handleChange}
            value={formik.values.confirmPassword}
          />
          {formik.errors.confirmPassword && <div className="error">{formik.errors.confirmPassword}</div>}
          <button
            type="submit"
            className="submit-btn text-white cursor-pointer"
          >
            REGISTER
          </button>
        </form>
      </div>
      <FileUploaderModal
        isOpen={isModalOpen}
        onClose={() => setIsModalOpen(false)}
        onCrop={handleCrop}
        image={file}
      />
    </div>
  );
}

export default FormSection;
