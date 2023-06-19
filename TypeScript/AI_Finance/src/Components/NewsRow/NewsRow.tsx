import style from './NewsRow.module.css'

export const NewsRow = (props: {title: string, content: string}) => {
    return(
        <div className={style.box}>
            <h3 className={style.title}>{props.title}</h3>
            <p>{props.content}</p>
        </div>
    );
}