import style from "./StockData.module.css";
import { DataRow } from "../DataRow/DataRow";
import { apiService } from "../../services/apiService";
import { useState, useEffect } from "react";

export const StockData = () => {
  const [data, setData] = useState([]);

  useEffect(() => {
    (async () => {
      try {
        setData(await apiService.get());
      } catch (error) {
        // eslint-disable-next-line no-console
        console.log(error);
        throw error;
      }
    })();
  }, []);

  const createRow = (
    item: { symbol: string; lastPrice: number },
    index: number
  ) => {
    return <DataRow key={index} symbol={item.symbol} price={item.lastPrice} />;
  };

  return (
    <div className={style.box}>
      <h1>Stocks</h1>
      {data.map(createRow)}
    </div>
  );
};
