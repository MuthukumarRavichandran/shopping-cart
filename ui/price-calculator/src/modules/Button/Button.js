import classNames from 'classnames';
import "./Button.scss";

const Button = ({ children, onClick, type = "primary", className = "" }) => {
    const buttonClassName = classNames('button',type,className);//`${styles.button} ${className}`
 return (
    <button className={buttonClassName} onClick={onClick}>
      {children}
    </button>
  );
};

export default Button;
