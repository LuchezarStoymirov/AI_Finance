import style from "./StockData.module.css";
import { DataRow } from "../DataRow/DataRow";
import { apiService } from "../../services/apiService";
import { useState, useEffect } from "react";

export const StockData = () => {
  const [data, setData] = useState([]);

  const fetchData = async () => {
    // eslint-disable-next-line no-useless-catch
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

  const exportFinancialData = async () => {
    // eslint-disable-next-line no-useless-catch
    try {
      const response = await apiService.exportData();
      const url = window.URL.createObjectURL(new Blob([response]));
      const link = document.createElement("a");
      link.href = url;
      link.setAttribute("download", "financial_data.csv");
      document.body.appendChild(link);
      link.click();
    } catch (error) {
      throw error;
    }
  };

  return (
    <div className={style.box}>
      <div className={style.titlebox}>
      <div className={style.titlecluster}>
          <h1 className={style.title}>Prices</h1>
          <button className={style.export} onClick={exportFinancialData}>
            Export
          </button>
          <input type="text" placeholder="Search..." className={style.search} />
          <button className={style.spyglass} onClick={exportFinancialData}>
            &#x1F50D;
          </button>
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
