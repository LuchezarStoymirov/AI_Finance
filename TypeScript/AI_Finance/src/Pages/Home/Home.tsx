import { Grid } from "@mui/material";
import style from "./Home.module.css";
import { useState, useEffect } from "react";
import { DataRow } from "../../Components/DataRow/DataRow";
import { News } from "../../Components/News/News";
import { StockData } from "../../Components/StockData/StockData";
import { Statements } from "../../Components/Statements/Statements";
import { Ratio } from "../../Components/Ratio/Ratio";
import { Analisys } from "../../Components/Analisys/Analisys";

export const Home = () => {
  const [data, setData] = useState([]);

  const fetchData = async () => {
    const response = await fetch("https://localhost:7085/Demo");
    const jsonData = await response.json();
    setData(jsonData);
  };

  useEffect(() => {
    fetchData();
  }, []);

  const createRow = (item: any, index: any) => {
    return <DataRow key={index} symbol={item.symbol} price={item.lastPrice} />;
  };

  console.log(data);

  return (
    <div className={style.container}>
      <Grid container>
        <Grid container lg={6} md={6} sm={12} className={style.gridrowOdd}>
          <StockData/>
        </Grid>
        <Grid container lg={6} md={6} sm={12} className={style.gridrowEven}>
          <News/>
        </Grid>
        <Grid container lg={6} md={6} sm={12} className={style.gridrowOdd}>
          <Statements/>
        </Grid>
        <Grid container lg={6} md={6} sm={12} className={style.gridrowEven}>
          <Ratio/>
        </Grid>
      </Grid>
      <Analisys/>
    </div>
  );
};
