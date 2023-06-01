import style from "./StockData.module.css";
import { DataRow } from "../DataRow/DataRow";
import { services } from "../../services/services";
import { useState, useEffect } from "react";

export const StockData = () => {
  const [data, setData] = useState([]);

  useEffect(() => {
    (async () => {
      setData(await services.get("https://localhost:7085/Demo"));
    })();
  }, []);

  const createRow = (item: { symbol: string; lastPrice: number; }, index: number) => {
    return <DataRow key={index} symbol={item.symbol} price={item.lastPrice} />;
  };

  return (
    <div className={style.box}>
      <h1>Stocks</h1>
      {data.map(createRow)}
    </div>
  );
};
