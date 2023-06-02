import {
  createBrowserRouter,
  createRoutesFromElements,
  Route,
  Outlet,
  RouterProvider,
  useLocation,
} from "react-router-dom";
import { Header } from "./Components/Header/Header";
import { Home } from "./Pages/Home/Home";
import { Login } from "./Pages/Login/Login";

import "./App.css";

function App() {
  const router = createBrowserRouter(
    createRoutesFromElements(
      <Route path="/" element={<Root />}>
        <Route index element={<Home />} />
        <Route path="/login" element={<Login/>}/>
      </Route>
    )
  );

  return (
    <div>
      <RouterProvider router={router} />
    </div>
  );
}

const Root = () => {
  const location = useLocation();
  const hideNavbar =
    location.pathname === "/register" || location.pathname === "/login";

  return (
    <>
      {!hideNavbar && <Header />}
      <div>
        <Outlet />
      </div>
    </>
  );
};

export default App;
