# This is an experiment with diagram

```mermaid
flowchart TD
  FE(View)
  SF(SearchFeature)
  ST[(Store)]
  DG(Data generator)
  FE -->|Give me some data| SF
  FE -->|Give me search result for this data| SF
  SF --> ST
  SF --> DG
```
