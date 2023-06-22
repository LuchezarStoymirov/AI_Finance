import style from "./StockData.module.css";
import { DataRow } from "../DataRow/DataRow";
import { apiService } from "../../services/apiService";
import { useState, useEffect } from "react";

export const StockData = () => {
  const [data, setData] = useState([]);

  const fetchData = async () => {
    try {
      const newData = await apiService.getStockData();
      setData(newData);
    } catch (error) {
      throw error;
    }
  };

  useEffect(() => {
    fetchData();

    const interval = setInterval(() => {
      fetchData();
    }, 5000);

    return () => {
      clearInterval(interval);
    };
  }, []);

  const createRow = (
    item: {
      imageUrl: string;
      name: string;
      price: string;
      marketCap: string;
      change: string;
    },
    id: number
  ) => {
    return (
      <DataRow
        key={id}
        picture={item.imageUrl}
        symbol={item.name}
        price={item.price}
        marketCap={item.marketCap}
        change={item.change}
      />
    );
  };

  return (
    <div className={style.box}>
      <div className={style.titlebox}>
        <div className={style.titlecluster}>
          <h1 className={style.title}>Prices</h1>
          <button className={style.export}>Export</button>
        </div>
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
