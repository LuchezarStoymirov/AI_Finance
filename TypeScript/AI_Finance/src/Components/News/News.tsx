import style from './News.module.css';
import { NewsRow } from '../NewsRow/NewsRow';
import { SimulatedNewsData } from '../../MockData/MockData';

export const News = () => {

  const CreateRow = (
    item: { title: string, article: string },
    id: number
  ) => {
    return <NewsRow
      key={id}
      title={item.title}
      content={item.article}
    />
  }

  return (
    <div className={style.box}>
      <div className={style.titlebox}>
        <h1 className={style.title}>News</h1>
      </div>
      {SimulatedNewsData.map(CreateRow)}
    </div>
  );
}