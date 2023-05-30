import { Grid } from "@mui/material";
import style from "./Home.module.css";
import { useState, useEffect } from "react";
import { DataRow } from "../../Components/DataRow/DataRow";

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
          <div className={style.box}>
            <h1>Stocks</h1>
            {data.map(createRow)}
          </div>
        </Grid>
        <Grid container lg={6} md={6} sm={12} className={style.gridrowEven}>
          <div className={style.box}>
            <h1>News</h1>
            <p>
              Lorem ipsum dolor sit amet consectetur adipisicing elit. Facilis
              earum labore cumque, architecto quos similique vitae optio nisi
              officiis ipsa provident nostrum quam velit tempora non debitis!
              Ipsum, ad similique?
            </p>
            <p>
              Lorem ipsum dolor sit amet consectetur adipisicing elit. Cumque
              sapiente nesciunt id dignissimos nulla ducimus aliquid quibusdam a
              itaque iste! Error consequatur autem at delectus repellendus
              voluptatibus earum quasi ea.
            </p>
            <p>
              Lorem ipsum dolor sit amet consectetur adipisicing elit. Minus vel
              laudantium deserunt reiciendis velit magnam eos ea iure, earum
              similique! Dolorem quidem porro accusantium omnis sequi? Laborum
              optio numquam molestiae!
            </p>
          </div>
        </Grid>
        <Grid container lg={6} md={6} sm={12} className={style.gridrowOdd}>
          <div className={style.box}></div>
        </Grid>
        <Grid container lg={6} md={6} sm={12} className={style.gridrowEven}>
          <div className={style.box}></div>
        </Grid>
      </Grid>
      <div className={style.info}></div>
    </div>
  );
};
