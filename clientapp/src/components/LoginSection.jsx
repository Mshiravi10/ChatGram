import React from "react";
import { useFormik } from "formik";

const validate = (values) => {
  const errors = {};

  if (!values.email) {
    errors.email = "Email is required";
  } else if (!/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i.test(values.email)) {
    errors.email = "Invalid email address";
  }

  if (!values.password) {
    errors.password = "Password is required";
  } else if (values.password.length < 8) {
    errors.password = "Password must be at least 8 characters";
  }

  return errors;
};

function LoginSection() {
  const formik = useFormik({
    initialValues: {
      email: "",
      password: "",
    },
    validate,
    onSubmit: (values) => {
      alert(JSON.stringify(values, null, 2));
    },
  });

  return (
    <div className="section-container">
      <div className="form-container">
        <form onSubmit={formik.handleSubmit}>
          <input
            type="email"
            placeholder="Email Address"
            name="email"
            id="email"
            onChange={formik.handleChange}
            value={formik.values.email}
          />
          {formik.errors.email ? (
            <div className="error">{formik.errors.email}</div>
          ) : null}
          <input
            type="password"
            placeholder="Password"
            name="password"
            id="password"
            onChange={formik.handleChange}
            value={formik.values.password}
          />
          {formik.errors.password ? (
            <div className="error">{formik.errors.password}</div>
          ) : null}
          <button
            type="submit"
            className="submit-btn text-white cursor-pointer"
          >
            LOG IN
          </button>
        </form>
        <p className="terms-text">
          Don't have an account?&nbsp;
          <a href="register" className="terms-link">
            Register
          </a>
        </p>
        <p className="terms-text">
          Forgot your password?&nbsp;
          <a href="reset" className="terms-link">
            Reset Password
          </a>
        </p>
      </div>
    </div>
  );
}

export default LoginSection;
