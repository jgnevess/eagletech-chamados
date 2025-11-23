import React from "react";



const Loading = () => (
  <div className="w-100 d-flex align-items-center justify-content-center" style={{ height: "100vh" }}>
    <div className="spinner-grow" style={
      {
        width: "3rem",
        height: "3rem"
      }
    } role="status">
      <span className="visually-hidden">Loading...</span>
    </div>
  </div>
)

export default Loading;