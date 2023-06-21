import style from "./StockData.module.css";
import { DataRow } from "../DataRow/DataRow";
import { apiService } from "../../services/apiService";
import { useState, useEffect } from "react";


export const StockData = () => {
  const [data, setData] = useState([]);


  useEffect(() => {
    (async () => {
      try {
        setData(await apiService.getStockData());
      } catch (error) {
        // eslint-disable-next-line no-console
        console.log(error);
        throw error;
      }
    })();
  }, []);

  const getStocks = () => {
    (async () => {
      try {
        setData(await apiService.getStockData());
      } catch (error) {
        // eslint-disable-next-line no-console
        console.log(error);
        throw error;
      }
    })();
  }

  setInterval(getStocks, 10000);

  const createRow = (
    item: { name: string; price: string, marketCap: string, change: string },
    id: number
  ) => {
    return <DataRow key={id} symbol={item.name} price={item.price} marketCap={item.marketCap} change={item.change} />;
  };

  return (
    <div className={style.box}>
      <div className={style.titlebox}>
        <h1 className={style.title}>Prices</h1>
        <div className={style.header}>
          <h4>Symbol</h4>
          <h4>Price</h4>
          <h4>Market Cap</h4>
          <h4>% Change</h4>
        </div>
      </div>
      {data.map(createRow)}
    </div>
  );
};
