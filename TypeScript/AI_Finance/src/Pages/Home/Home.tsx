import { Grid } from "@mui/material";
import style from "./Home.module.css";
import { News } from "../../Components/News/News";
import { StockData } from "../../Components/StockData/StockData";
import { Statements } from "../../Components/Statements/Statements";
import { Ratio } from "../../Components/Ratio/Ratio";
import { Analisys } from "../../Components/Analisys/Analisys";

export const Home = () => {

  return (
    <div className={style.container}>
      <Grid container>
        <Grid item container lg={6} md={6} sm={12} className={style.gridrowOdd}>
          <StockData/>
        </Grid>
        <Grid item container lg={6} md={6} sm={12} className={style.gridrowEven}>
          <News/>
        </Grid>
        <Grid item container lg={6} md={6} sm={12} className={style.gridrowOdd}>
          <Statements/>
        </Grid>
        <Grid item container lg={6} md={6} sm={12} className={style.gridrowEven}>
          <Ratio/>
        </Grid>
      </Grid>
      <Analisys/>
    </div>
  );
};
