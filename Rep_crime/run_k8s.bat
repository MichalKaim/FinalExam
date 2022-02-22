CHDIR %~dp0
kubectl apply -f mongo.yml
kubectl apply -f crimeapi.yml
kubectl apply -f lawenforcementapi.yml
kubectl apply -f rabbitmq.yaml
pause