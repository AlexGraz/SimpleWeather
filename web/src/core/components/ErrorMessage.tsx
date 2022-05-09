import styled from "styled-components";

const Container = styled.div`
  padding: 0.5rem;
  width: 100%;
  color: #c62828;
  background-color: #ffebee;
  border: 1px solid #c62828;
  font-weight: bold;
`;

export function ErrorMessage({ error }: { error: string | undefined }) {
  return error ? <Container>{error}</Container> : <></>;
}
