import { useEffect } from 'react';
import style from "./Home.module.css";
import { News } from "../../Components/News/News";
import { StockData } from "../../Components/StockData/StockData";
import { Statements } from "../../Components/Statements/Statements";
import { Ratio } from "../../Components/Ratio/Ratio";
import { Analisys } from "../../Components/Analisys/Analisys";
import { Grid } from "@mui/material";

export const Home = () => {

  useEffect(() => {
    const token = localStorage.getItem("token");
    if (!token) {
      window.location.href = "/login";
    }
  }, []);

  return (
    <div className={style.container}>
      <Grid container>
        <Grid item container lg={6} md={6} sm={12} className={style.gridrowOdd}>
          <StockData/>
        </Grid>
        <Grid item container lg={6} md={6} sm={12} className={style.gridrowEven}>
          <News/>
        </Grid>
      </Grid>
      <Analisys/>
    </div>
  );
};
