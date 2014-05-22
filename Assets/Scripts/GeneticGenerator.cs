using UnityEngine;
using System.Collections;


public class GeneticGenerator : MonoBehaviour
{
    /*Plant[] population;
    int populationSize;         // taille de la population
    int numberOfAttributes;     // nombre de chromosomes par individu
    int sequenceLength;         // longueur des chromosomes
    float mutationProbability;  // probabilité de mutation 
    float elitism;              // probabilité que le vainqueur d"un affrontement soit le plus adapté des deux
 

    void Start()
    {
        populationSize = 20;
        numberOfAttributes = 4;
        sequenceLength = 10;
        mutationProbability = 0.05f;
        elitism = 0.75f;
        population = generateRandomPopulation(populationSize,numberOfAttributes,sequenceLength);
    }

    void Update()
    {

    }

    void Run()
    {   

        Debug.Log("!!!!!! NOUVELLE PARTIE !!!!!!");

        
        int nbOfTurn = 0;

        int avgFitness = 0;  // coefficient d'adaptation moyen de la population
        int maxFitness = 0; // coefficient d'adaptation maximal
        Plant winner = null; // individu ayant ce coefficient maximal

        while (maxFitness<3700)
        {
            nbOfTurn += 1;
            maxFitness = 0;
            int sumFitness = 0;
            //for i in population
            for (int i=0; i<populationSize; i++)
            {
                evaluateFitness(population[i]);
                sumFitness += population[i].fitness;
                if (population[i].fitness>maxFitness)
                {
                    maxFitness=population[i].fitness;
                    winner = population[i];
                }
            }
            avgFitness = sumFitness/populationSize;
    
            Debug.Log("//" + nbOfTurn.ToString());
            Debug.Log("maximum fitness = " + maxFitness.ToString() ); //+ "   average fitness = " + str(avgFitness) )     
    
            // selection des parents par tournois succesifs parmi la population
            Plant[] parents = selectParents(elitism);
            temporaryParents = parents // copie de la liste utilisée pour la condition d'arrêt de la boucle while qui suit
    
            children = [] 
            while (len(temporaryParents)>0) :
                index1 = randint(0,len(temporaryParents)-1)
                index2 = randint(0,len(temporaryParents)-1)
                while (index2==index1) : index2=randint(0,len(temporaryParents)-1)
                splitIndex = randint(0,sequenceLength-1)
                child1,child2 = crossIndividuals(temporaryParents[index1],temporaryParents[index2],splitIndex)
                children.append(child1)
                children.append(child2)
                if (index1>index2): index1,index2 = index2,index1
                temporaryParents.pop(index2)
                temporaryParents.pop(index1)
                //Debug.Log("Il reste " + str(len(parents)) + " parents")
    
            // Seuls les enfants ont une probabilité de muter    
            mutatePopulation(children,mutationProbability)
    
            // On peut choisir le pourcentage de parents qui seront remplacés par leurs enfants.
            for c in children:
                population.append(c);
        }
        
        Debug.Log("le gagnant est : \n" + str(winner))

    }

    //import bpy
    //from math import cos, sin, tan, acos, asin, atan, pi, sqrt
    from bpy.props import *
    from random import randint, random, uniform

    // variable globale, contenant la population du problème
    List<Plant> population;


    //fonctions utilisées dans l'algorithme génétique

    Plant[] generateRandomPopulation(int populationSize, int numberOfAttributes, int sequenceLength)
    {
        Plant[] randomPopulation = new Plant[populationSize];
        for (int i=0; i<populationSize; i++)
        {
            short[][] attributes = new short[numberOfAttributes][];
            for j in range(numberOfAttributes)
            {
                newAttribute = []  
                for k in range(sequenceLength):
                    newAttribute.append(randint(0,1))
                attributes.append(newAttribute)
            }
            newIndividual = individual(attributes)  
            population.append(newIndividual)
        }
        return 
    }
             
    //def printPopulation():
    //    for i in population:
    //        Debug.Log(i)
    //    Debug.Log('\n\n')
    
    // fonction d'évaluation d'un individu (exemple faisant la somme des valeurs des attributs)    
    float evaluateFitness(Plant individual)
    {
        // implémenter le déroulement du jeu, qui renvoie le score final de la plante
    
        //individual.fitness = 0
        //for a in individual.attributes:
        //    attributeValue = 0
        //    reversedAttribute = reversed(a)
        //    index = 0
        //    for bit in reversedAttribute :
        //        if bit==1: attributeValue += pow(2,index)
        //        index+=1
        //    individual.fitness += attributeValue
    
        return individual.fitness;
    }

    // sélection des parents (doit etre utilisée sur une population de taille paire)
    // requierant un parametre compris entre 0.5 et 1.0
    Plant[] selectParents(elitism)
    {
        int populationSize = population.Length;
        Plant[] newParents = new Plant[2];
        while (population.Length()>populationSize/2):
            index1=randint(0,population.Length()-1)
            index2=randint(0,population.Length()-1)
            while (index2==index1) : index2=randint(0,population.Length()-1)
            if(index1==index2):Debug.Log('Attention : meme parent choisi 2 fois !')
        
            if (population[index1].fitness < population[index2].fitness):
                population[index1],population[index2] = population[index2],population[index1]
            // on est donc maintenant sûr que l'individu le mieux adapté est à la position index
            r = random()
            if (r<elitism):
                newParents.append(population[index1])
                population.pop(index1)
            else :
                newParents.append(population[index2])
                population.pop(index2)
        return newParents;
    }
        

    def mutatePopulation(_population,mutationProbability):
        for i in _population:
            for j in i.attributes:
                r = random()
                if (r < mutationProbability):
                    //Debug.Log('Mutation :\n')
                    //Debug.Log('   ' + str(i))
                    index = randint(0,len(j)-1)
                    if (j[index]==0) : j[index]=1
                    else : j[index]=0
                    //Debug.Log('   ' + str(i))

    // croisement entre 2 individus, avec un seul point de croisement, à la position splitIndex
    def crossIndividuals(Plant indiv1, Plant indiv2, int splitIndex):
        child1Attributes = [];
        child2Attributes = [];
    
        for (int i=0; i<len(indiv1.attributes); i++)
        {
            child1NewAttribute = crossSequences(indiv1.attributes[i],indiv2.attributes[i],splitIndex);
            child1Attributes.append(child1NewAttribute);
            child2NewAttribute = crossSequences(indiv2.attributes[i],indiv1.attributes[i],splitIndex);
            child2Attributes.append(child2NewAttribute);
        }
        Plant child1 = individual(child1Attributes);
        Plant child2 = individual(child2Attributes);
        return child1,child2;
    
    
    Sequence crossSequences(seq1,seq2,splitIndex)
    {
        newSequence=[]
        for (int i=0; i<len(seq1); i++)
        {
            if(i<splitIndex) newSequence.append(seq1[i]);
        
            else newSequence.append(seq2[i]);
        }
        return newSequence;
    }*/
    
}