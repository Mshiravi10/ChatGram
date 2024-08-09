import "./App.css";
import TextSection from "./components/TextSection";
import FormSection from "./components/FormSection";
import LoginSection from "./components/LoginSection"; // Import LoginSection

function App() {
  const isLogin = false; // Toggle between true and false to switch between login and register

  return (
    <div className="app">
      <TextSection />
      {isLogin ? <LoginSection /> : <FormSection />}
    </div>
  );
}

export default App;
