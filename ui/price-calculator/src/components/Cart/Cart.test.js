import { render, screen } from "@testing-library/jest-dom";
import Chart from "./Chart";

describe("Chart Component", () => {
  const sampleData = [
    { item: "Apple", quantity: 2, rate: 50, discount: 10, price: 90 },
    { item: "Orange", quantity: 1, rate: 30, discount: 0, price: 30 },
  ];

  test("renders loading state", () => {
    render(<Chart apiResponse={[]} loading={true} />);
    expect(screen.getByText("Data loading")).toBeInTheDocument();
  });

  test("renders no data state", () => {
    render(<Chart apiResponse={[]} loading={false} />);
    expect(screen.getByText("No data to display")).toBeInTheDocument();
  });

  test("renders table with data and calculates total", () => {
    render(<Chart apiResponse={sampleData} loading={false} />);

    expect(screen.getByText("S.No,")).toBeInTheDocument();
    expect(screen.getByText("Iteam Name")).toBeInTheDocument();
    expect(screen.getByText("Quantity")).toBeInTheDocument();
    expect(screen.getByText("Rate")).toBeInTheDocument();
    expect(screen.getByText("Discount")).toBeInTheDocument();
    expect(screen.getByText("Final Amount")).toBeInTheDocument();

    expect(screen.getByText("Apple")).toBeInTheDocument();
    expect(screen.getByText("Orange")).toBeInTheDocument();
    expect(screen.getByText("2")).toBeInTheDocument();
    expect(screen.getByText("50")).toBeInTheDocument();
    expect(screen.getByText("10%")).toBeInTheDocument();
    expect(screen.getByText("90")).toBeInTheDocument();
    expect(screen.getByText("Total Amount")).toBeInTheDocument();
  });
});
