import style from "./StockData.module.css";
import { DataRow } from "../DataRow/DataRow";
import { fetchWrapper } from "../../services/fetchWrapper";
import { useState, useEffect } from "react";

export const StockData = () => {
  const [data, setData] = useState([]);

  useEffect(() => {
    (async () => {
      setData(await fetchWrapper.get("https://localhost:7085/Demo"));
    })();
  }, []);

  const createRow = (item: any, index: any) => {
    return <DataRow key={index} symbol={item.symbol} price={item.lastPrice} />;
  };

  return (
    <div className={style.box}>
      <h1>Stocks</h1>
      {data.map(createRow)}
    </div>
  );
};
