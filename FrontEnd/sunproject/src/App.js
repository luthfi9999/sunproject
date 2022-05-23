import SideBar from "./components/SideBar";
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'
import Home from "./pages/Home";
import Promotion from "./pages/Promotion";
import About from "./pages/About";
import './App.css';

function App() {
  return (
    <div className="App">
      <Router>
        <SideBar />
        <Routes>
          <Route path='/' exact element={<Home/>} />
          <Route path='/about' element={<About/>} />
          <Route path='/promotion' element={<Promotion/>} />
        </Routes>
      </Router>
    </div>
  );
}

export default App;
