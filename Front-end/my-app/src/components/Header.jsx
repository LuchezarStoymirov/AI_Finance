import React from "react";
import { Grid } from "@mui/material";
import { Link } from "react-router-dom";

export const Header = () => {
  return (
    <div className="header">
      <Grid container className="header-container">
        <Grid item>
          <img
            src="src/images/CashGrab-logo.png"
            alt="CashGrab logo"
            className="logo-image"
            style={{ width: "100%", maxWidth: "200px" }}
          />
        </Grid>
        <Grid container justifyContent="flex-end" alignItems="center">
          <Grid item className="header-link">
            <Link to="/" className="header-link__item">
              Home
            </Link>
          </Grid>
          <Grid item className="header-link">
            <Link to="/data" className="header-link__item">
              Data
            </Link>
          </Grid>
          <Grid item className="header-link">
            <Link to="/profile" className="header-link__item">
              Profile
            </Link>
          </Grid>
        </Grid>
      </Grid>
    </div>
  );
};
