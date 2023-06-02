import { Grid } from "@mui/material";
import { Link } from "react-router-dom";
import './Header.css';

export const Header = () => {
  return (
      <Grid container className="header" alignItems="center">
        <Grid item>
          <img
            src="src/images/CashGrab-logo-light.png"
            alt="CashGrab logo"
            className="logo-image"
          />
        </Grid>
        <Grid item sx={{ marginLeft: 'auto' }}>
          <Link to="/" className="header-link__item">
            Home
          </Link>
          <Link to="/profile" className="header-link__item">
            Profile
          </Link>
        </Grid>
      </Grid>
  );
};
