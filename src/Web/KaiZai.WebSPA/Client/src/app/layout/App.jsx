import "../../features/style.css";
import "./App.css";
import { Container } from "semantic-ui-react";
import { Outlet, useLocation } from "react-router-dom";
import MainMenu from "../layout/MainMenu";
import Login from "../../features/account/Login";

function App() {
  const location = useLocation();
  return (
    //TODO: add centering for big resolutions and styles for different resolutions
    <>
      {location.pathname !== "/login" ? (
        <div id="app-main-features-container">
          <MainMenu />
          <Container id="app-feature-related-container">
            {location.pathname === "/" ? <div>Hi</div> : <Outlet />}
          </Container>
        </div>
      ) : (
        <Login />
      )}
    </>
  );
}

export default App;
