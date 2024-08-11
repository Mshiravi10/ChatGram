import React from "react";
import { FaComments, FaUsers, FaShieldAlt, FaMobileAlt } from "react-icons/fa"; // Import some icons from react-icons
import './styles/HomeSection.css'

function HomeSection() {
  return (
    <div className="text-container text-white text-center">
      <h2 className="heading">
        Connect with friends
        <br />
        through ChatGram
      </h2>
      <p className="paragraph">
        Chat with your friends in real-time, share your thoughts, and stay connected wherever you are. Experience seamless communication with our intuitive platform.
      </p>
      
      {/* Feature Section */}
      <div className="features">
        <div className="feature-item">
          <FaComments size={50} />
          <h4>Real-Time Messaging</h4>
          <p>Instant communication with your friends and family.</p>
        </div>
        <div className="feature-item">
          <FaUsers size={50} />
          <h4>Group Chats</h4>
          <p>Create and join groups to stay connected with multiple people at once.</p>
        </div>
        <div className="feature-item">
          <FaShieldAlt size={50} />
          <h4>Privacy and Security</h4>
          <p>Your conversations are safe with end-to-end encryption.</p>
        </div>
        <div className="feature-item">
          <FaMobileAlt size={50} />
          <h4>Accessible Anywhere</h4>
          <p>Use ChatGram on any device, anytime, anywhere.</p>
        </div>
      </div>
    </div>
  );
}

export default HomeSection;
